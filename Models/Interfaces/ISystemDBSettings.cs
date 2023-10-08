namespace TrainBookingBackend.Models.Interfaces
{
    public interface ISystemDBSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
        string CollectionName { get; set; }
    }
}
