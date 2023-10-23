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

        [BsonElement("route")]
        public List<string> Route { get; set; } = new List<string>();

        [BsonElement("pricePerBlock")]
        public float PricePerBlock { get; set; }

        [BsonElement("startTime")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [BsonElement("endTime")]
        public DateTime EndTime {  get; set; } = DateTime.Now;

        [BsonElement("effectiveDate")]
        public DateTime EffectiveDate { get; set; } = DateTime.Now;
    }
}
