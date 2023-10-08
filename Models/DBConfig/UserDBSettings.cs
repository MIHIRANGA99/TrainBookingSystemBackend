using TrainBookingBackend.Models.Interfaces;

namespace TrainBookingBackend.Models.DBConfig
{
    public class UserDBSettings : IUserDBSettings
    {
        public string UserDatabaseName { get; set; } = String.Empty;
        public string UserConnectionString { get; set; } = String.Empty;
        public string UserCollectionName { get; set; } = String.Empty;
    }
}
