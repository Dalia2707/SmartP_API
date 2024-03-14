using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class Electrovalve
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;} = string.Empty;

        [BsonElement("Open")]
        public bool Open {get; set;}

        [BsonElement("Date")]
        public DateTime Date {get; set;}
    }
}