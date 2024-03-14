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
    public class SizeServices
    {
        public readonly IMongoCollection<Size> _SizeCollection;

        public SizeServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _SizeCollection = mongoDB.GetCollection<Size>("SizeSensor");

            //_UserCollection = mongoDB.GetCollection<User>(databaseSettings.Value.CollectionName);
        }
       
      public async Task<List<Size>> GetAsync()
        {
            var size = await _SizeCollection.Find(_ => true).ToListAsync();

            size.ForEach(sizes =>Convert.ToString(sizes.Id));

            return size;
        }
        public async Task InsertSize(Size SizeInsert)
        {
            await _SizeCollection.InsertOneAsync(SizeInsert);
        }

        public async Task DeleteSize(string SizeId)
        {
            var filter = Builders<Size>.Filter.Eq(s=>s.Id, SizeId);
            await _SizeCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateSize(Size dataToUpdate)
        {
            var filter = Builders<Size>.Filter.Eq(s=>s.Id, dataToUpdate.Id);
            await _SizeCollection.ReplaceOneAsync(filter,dataToUpdate);
        }
    }
}