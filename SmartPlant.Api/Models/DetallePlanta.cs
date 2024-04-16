using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SmartPlant.Api.Models
{
    public class DetallePlanta
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        ////Planta
        [BsonIgnore]
        public Plant? Planta { get; set; }

        [BsonElement("Plant")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Plant { get; set; } = string.Empty;

        //Humedad
        //[BsonIgnore]
        //public Hum Hum { get; set; }

        [BsonElement("Humidity")]      
        public double Humidity { get; set; }=0;

        ////Tamaño
        //[BsonIgnore]
        //public Size Size { get; set; }

        //[BsonElement("Tamaño")]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string Tamaño { get; set; }=string.Empty;

        //Electrovalvula
     
        [BsonElement("Electrovalve")]      
        public bool Electrovalve { get; set; } = false;
    }
}
