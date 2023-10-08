using TrainBookingBackend.Models.Interfaces;
namespace TrainBookingBackend.Models.DBConfig
{
    public class SystemDBSettings : ISystemDBSettings
    {
        public string DatabaseName { get; set; } = String.Empty;
        public string ConnectionString { get; set; } = String.Empty;
        public string CollectionName { get; set; } = String.Empty;
    }
}
