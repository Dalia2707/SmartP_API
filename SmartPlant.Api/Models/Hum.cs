using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class Hum
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;} = string.Empty;

        [BsonElement("Humidity")]
        public double Humidity {get; set;}

    }
}