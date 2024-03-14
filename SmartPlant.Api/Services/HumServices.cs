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
    public class HumServices
    {
        public readonly IMongoCollection<Hum> _HumCollection;

        public HumServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _HumCollection = mongoDB.GetCollection<Hum>("HumiditySensor");

            //_UserCollection = mongoDB.GetCollection<User>(databaseSettings.Value.CollectionName);
        }
       
      public async Task<List<Hum>> GetAsync()
        {
            var hum = await _HumCollection.Find(_ => true).ToListAsync();

            hum.ForEach(hume =>Convert.ToString(hume.Id));

            return hum;
        }
        public async Task InsertHum(Hum HumInsert)
        {
            await _HumCollection.InsertOneAsync(HumInsert);
        }

        public async Task DeleteHum(string HumId)
        {
            var filter = Builders<Hum>.Filter.Eq(s=>s.Id, HumId);
            await _HumCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateHum(Hum dataToUpdate)
        {
            var filter = Builders<Hum>.Filter.Eq(s=>s.Id, dataToUpdate.Id);
            await _HumCollection.ReplaceOneAsync(filter,dataToUpdate);
        }
    }
}