using TrainBookingBackend.Models.Interfaces;

namespace TrainBookingBackend.Models.DBConfig
{
    public class ScheduleDBSettings : IScheduleDBSettings
    {
        public string ScheduleDatabaseName { get; set; } = string.Empty;
        public string ScheduleConnectionString { get; set; } = string.Empty;
        public string ScheduleCollectionName { get; set; } = string.Empty;
    }
}
