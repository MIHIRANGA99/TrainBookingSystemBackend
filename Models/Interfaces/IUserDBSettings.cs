namespace TrainBookingBackend.Models.Interfaces
{
    public interface IUserDBSettings
    {
        string UserDatabaseName { get; set; }
        string UserConnectionString { get; set; }
        string UserCollectionName { get; set; }
    }
}
