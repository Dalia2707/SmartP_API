using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class Size
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id {get; set;} = string.Empty;

        [BsonElement("Size")]
        public double tam {get; set;}

    }
}