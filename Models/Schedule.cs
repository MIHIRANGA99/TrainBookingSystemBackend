using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Annotations;

namespace TrainBookingBackend.Models
{
    [BsonIgnoreExtraElements]
    public class Schedule
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("trainId")]
        public string TrainId { get; set; } = string.Empty;

        [BsonElement("startLocation")]
        public string StartLocation { get; set; } = string.Empty;

        [BsonElement("endLocation")]
        public string EndLocation { get; set; } = string.Empty;

        [BsonElement("price")]
        public float Price { get; set; }
        // public List<DateManagement> DateMgt { get; set; }
    }
}
