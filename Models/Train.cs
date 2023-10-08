using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Annotations;

namespace TrainBookingBackend.Models
{
    [BsonIgnoreExtraElements]
    public class Train
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("trainName")]
        public string TrainName { get; set; } = string.Empty;

        [BsonElement("trainType")]
        public string TrainType { get; set; } = string.Empty;

        [BsonElement("schedules")]
        public List<string> Schedules { get; set; } = new List<string>();

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("isPublished")]
        public bool IsPublished { get; set; }
    }
}
