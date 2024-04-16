using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using SmartPlant.Api.Configurations;
using SmartPlant.Api.Models;

namespace SmartPlant.Api.Services
{
    public class DetallePlantaServices
    {
        private readonly IMongoCollection<DetallePlanta> _DetallePlantaCollection;
        private readonly IMongoCollection<Plant> _PlantCollection;
        //private readonly IMongoCollection<Hum> _HumCollection;
        //private readonly IMongoCollection<Size> _SizeCollection;
        //private readonly IMongoCollection<Electrovalve> _ElectrovalveCollection;
        
        public DetallePlantaServices(IOptions<DatabaseSettings>databasesettings)
        {
            var mongoClient = new MongoClient(databasesettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(databasesettings.Value.DatabaseName);
            _DetallePlantaCollection = mongoDB.GetCollection<DetallePlanta>(databasesettings.Value.DetalleplantaCollection);
            _PlantCollection = mongoDB.GetCollection<Plant>(databasesettings.Value.PlantaCollection);
            //_HumCollection = mongoDB.GetCollection<Hum>(databasesettings.Value.PlantaCollection);
            //_SizeCollection = mongoDB.GetCollection<Size>(databasesettings.Value.TamañoColllection);
            //_ElectrovalveCollection = mongoDB.GetCollection<Electrovalve>(databasesettings.Value.ElectovalvulaColection);
        }

        public async Task<List<DetallePlanta>> GetAsync()
        {
            var detalleplanta = await _DetallePlantaCollection.Find(_ => true).ToListAsync();
            return detalleplanta;
        }

        public async Task UpdateDetallePlant(DetallePlanta dataToUpdate)
        {
            var filter = Builders<DetallePlanta>.Filter.Eq(s => s.Id, dataToUpdate.Id);
            await _DetallePlantaCollection.ReplaceOneAsync(filter, dataToUpdate);
        }

        public async Task InsertarDetallePlant(DetallePlanta detallePlanta)
        {
            await _DetallePlantaCollection.InsertOneAsync(detallePlanta);
        }
       
        public async Task<DetallePlanta>DetalleId(string Id)
        {
            var detalle = await _DetallePlantaCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id) } }).Result.FirstAsync();
            //var planta = await _PlantCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Planta) } }).Result.FirstAsync();
            //var Humendad = await _HumCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Humedad) } }).Result.FirstAsync();
            //var Tamaño = await _SizeCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Tamaño) } }).Result.FirstAsync();
            //var electrovalvula = await _ElectrovalveCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Electrovalvula) } }).Result.FirstAsync();
            //detalle.Plant = planta;
            //detalle.Hum = Humendad;
            //detalle.Size = Tamaño;
            //detalle.Electrovalve = electrovalvula;
            return detalle;

        }

        public async Task<DetallePlanta> detalleporIdPlant(string Id)
        {
            var detalle = await _DetallePlantaCollection.FindAsync(new BsonDocument { { "Plant", new ObjectId(Id) } }).Result.FirstOrDefaultAsync();
            if (detalle != null)
            {
                var planta = await _PlantCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Plant) } }).Result.FirstOrDefaultAsync();
                //var Humendad = await _HumCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Humedad) } }).Result.FirstAsync();
                //var Tamaño = await _SizeCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Tamaño) } }).Result.FirstAsync();
                //var electrovalvula = await _ElectrovalveCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(detalle.Electrovalvula) } }).Result.FirstAsync();
                detalle.Planta = planta;
                //detalle.Hum = Humendad;
                //detalle.Size = Tamaño;
                //detalle.Electrovalve = electrovalvula;
            }
            return detalle;

        }


        
    }
}
