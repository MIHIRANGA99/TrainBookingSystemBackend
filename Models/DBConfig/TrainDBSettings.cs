using TrainBookingBackend.Models.Interfaces;

namespace TrainBookingBackend.Models.DBConfig
{
    public class TrainDBSettings : ITrainDBSettings
    {
        public string TrainDatabaseName { get; set; } = String.Empty;
        public string TrainConnectionString { get; set; } = String.Empty;
        public string TrainCollectionName { get; set; } = String.Empty;
    }
}
