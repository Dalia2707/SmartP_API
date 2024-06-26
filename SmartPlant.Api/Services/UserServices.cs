using MongoDB.Driver;
using SmartPlant.Api.Models;
using SmartPlant.Api.Configurations;
using Microsoft.Extensions.Options;
using ZstdSharp.Unsafe;
using MongoDB.Bson;

using MongoDB.Bson.Serialization;
using System.Diagnostics;
using System.Linq.Expressions;

namespace SmartPlant.Api.Services
{
    public class UserServices
    {
        public readonly IMongoCollection<User> _UserCollection;

        public UserServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _UserCollection = mongoDB.GetCollection<User>(databaseSettings.Value.UsersCollection);
        }

        public async Task<List<User>> GetAsync() =>
            await _UserCollection.Find(_ => true).ToListAsync();

        public async Task<User> GetUser(string emailUser, string password)
        {
            var filterDefinition = Builders<User>.Filter.Where(x=>x.EmailUser==emailUser && x.Password==password);
            return await _UserCollection.FindAsync(filterDefinition).Result.FirstOrDefaultAsync();
        }

        public async Task<User> GetUser(string emailUser)
        {
            var filterDefinition = Builders<User>.Filter.Where(x => x.EmailUser == emailUser);
            return await _UserCollection.FindAsync(filterDefinition).Result.FirstOrDefaultAsync();
        }

        public async Task InsertUser(User userInsert)
        {
            await _UserCollection.InsertOneAsync(userInsert);
            string id = userInsert.Id;
        }

        public async Task DeleteUser(string userId)
        {
            var filter = Builders<User>.Filter.Eq(s=>s.Id, userId);
            await _UserCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateUser(User dataToUpdate)
        {
            var filter = Builders<User>.Filter.Eq(s=>s.Id, dataToUpdate.Id);
            await _UserCollection.ReplaceOneAsync(filter,dataToUpdate);
        }

        public async Task<User> GetUserId(string Id)
        {
            return await _UserCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(Id)}}).Result.FirstAsync();
        }
    }
}