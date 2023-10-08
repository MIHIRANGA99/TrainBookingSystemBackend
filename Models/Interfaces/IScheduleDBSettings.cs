namespace TrainBookingBackend.Models.Interfaces
{
    public interface IScheduleDBSettings
    {
        string ScheduleDatabaseName { get; set; }
        string ScheduleConnectionString { get; set; }
        string ScheduleCollectionName { get; set; }
    }
}
