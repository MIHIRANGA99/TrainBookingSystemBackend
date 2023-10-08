using TrainBookingBackend.Models;

namespace TrainBookingBackend.Services.Interfaces
{
    public interface ITrainService
    {
        List<Train> GetTrains();
        Train GetTrain(string id);
        Train CreateTrain(Train train);
        Train UpdateTrain(string id, Train train);
        void DeleteTrain(string id);
    }
}
