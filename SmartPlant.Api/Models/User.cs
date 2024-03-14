using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;} = string.Empty;

        [BsonElement("NameUser")]
        public string NameUser {get; set;} = string.Empty;

        [BsonElement("EmailUser")]
        public string EmailUser {get; set;} = string.Empty;

        [BsonElement("Password")]
        public string Password {get; set;} = string.Empty;
    }
}