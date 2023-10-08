using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace TrainBookingBackend.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;

        [BsonElement("nic")]
        public string NIC { get; set; } = String.Empty;

        [BsonElement("firstName")]
        public string FirstName { get; set; } = String.Empty;

        [BsonElement("lastName")]
        public string LastName { get; set; } = String.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = String.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = String.Empty;

        [BsonElement("mobileNumber")]
        public string MobileNumber { get; set; } = String.Empty;

        [BsonElement("role")]
        public string Role { get; set; } = String.Empty;

        [BsonElement("isActive")]
        public bool IsActive {  get; set; }
    }
}
