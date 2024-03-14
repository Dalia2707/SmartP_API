using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class DetallePlanta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        //Planta
        [BsonIgnore]
        public Plant Plant { get; set; }

        [BsonElement("Planta")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Planta { get; set; } = string.Empty;

        //Humedad
        [BsonIgnore]
        public Hum Hum { get; set; }

        [BsonElement("Humedad")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Humedad { get; set; }=string.Empty;

        //Tamaño
        [BsonIgnore]
        public Size Size { get; set; }

        [BsonElement("Tamaño")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Tamaño { get; set; }=string.Empty;

        //Electrovalvula
        [BsonIgnore]
        public Electrovalve Electrovalve { get; set;}

        [BsonElement("Electrovalvula")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Electrovalvula { get; set; } = string.Empty;
    }
}
