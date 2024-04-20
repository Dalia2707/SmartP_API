using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class Plant
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public string Id { get; set; } = string.Empty;

        [BsonElement("UsersId")] // Nombre del campo que almacenará el ID del usuario
        [BsonRepresentation(BsonType.ObjectId)]
        public string UsersId { get; set; } = string.Empty;

        [BsonElement("NamePlant")]
        public string NamePlant {get; set;} = string.Empty;

        [BsonElement("TypePlant")]
        public string TypePlant {get; set;} = string.Empty;

        [BsonIgnore]
        public User? User { get; set; }
       
        [BsonElement("ImagenURL")]
        public string ImagenURL {  get; set; }

    }
}