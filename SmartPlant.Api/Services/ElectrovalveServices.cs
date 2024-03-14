using MongoDB.Driver;
using SmartPlant.Api.Models;
using SmartPlant.Api.Configurations;
using Microsoft.Extensions.Options;
using ZstdSharp.Unsafe;
using MongoDB.Bson;

using MongoDB.Bson.Serialization;
using System.Diagnostics;

namespace SmartPlant.Api.Services
{
    public class ElectrovalveServices
    {
        public readonly IMongoCollection<Electrovalve> _ElectrovalveCollection;

        public ElectrovalveServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _ElectrovalveCollection = mongoDB.GetCollection<Electrovalve>("Electrovalve");

            //_UserCollection = mongoDB.GetCollection<User>(databaseSettings.Value.CollectionName);
        }
       
      public async Task<List<Electrovalve>> GetAsync()
        {
            var electrovalves = await _ElectrovalveCollection.Find(_ => true).ToListAsync();

            electrovalves.ForEach(electrovalve =>Convert.ToString(electrovalve.Id));

            return electrovalves;
        }
        public async Task InsertEle(Electrovalve EleInsert)
        {
            await _ElectrovalveCollection.InsertOneAsync(EleInsert);
        }

        public async Task DeleteEle(string EleId)
        {
            var filter = Builders<Electrovalve>.Filter.Eq(s=>s.Id, EleId);
            await _ElectrovalveCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateEle(Electrovalve dataToUpdate)
        {
            var filter = Builders<Electrovalve>.Filter.Eq(s=>s.Id, dataToUpdate.Id);
            await _ElectrovalveCollection.ReplaceOneAsync(filter,dataToUpdate);
        }
    }
}