namespace TrainBookingBackend.Models.Interfaces
{
    public interface ITrainDBSettings
    {
        string TrainDatabaseName { get; set; }
        string TrainConnectionString { get; set; }
        string TrainCollectionName { get; set; }
    }
}
