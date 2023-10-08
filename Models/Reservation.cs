using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace TrainBookingBackend.Models
{
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [SwaggerSchema(ReadOnly = true)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("userId")]
        public string UserId { get; set; } = String.Empty;

        [BsonElement("trainId")]
        public string TrainId { get; set; } = String.Empty;

        [BsonElement("bookingDate")]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [BsonElement("ticketPrice")]
        public float TicketPrice { get; set; } = 0;

        [BsonElement("seatNumber")]
        public int SeatNumber { get; set; }

        [BsonElement("status")]
        public int Status { get; set; }
    }
}
