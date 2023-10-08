using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TrainBookingBackend.Models;
using TrainBookingBackend.Models.DBConfig;
using TrainBookingBackend.Models.Interfaces;
using TrainBookingBackend.Services.Interfaces;

namespace TrainBookingBackend.Services
{
    public class TrainService : ITrainService
    {
        private readonly IMongoCollection<Train> _trains;
        public TrainService(ITrainDBSettings dbSettings, IMongoClient mongoClient)
        {
            var databaseName = dbSettings.TrainDatabaseName;
            var collectionName = dbSettings.TrainCollectionName;

            var database = mongoClient.GetDatabase(databaseName);
            _trains = database.GetCollection<Train>(collectionName);
        }
        public Train CreateTrain(Train train)
        {
            _trains.InsertOne(train);
            return train;
        }

        public void DeleteTrain(string id)
        {
            _trains.DeleteOne(id);
        }

        public Train GetTrain(string id)
        {
            return _trains.Find(train => train.Id == id).FirstOrDefault<Train>();
        }

        public List<Train> GetTrains()
        {
            return _trains.Find(reservation => true).ToList();
        }

        public Train UpdateTrain(string id, Train train)
        {
            _trains.ReplaceOne(train => train.Id == id, train);
            return _trains.Find(train => train.Id == id).FirstOrDefault<Train>();
        }
    }
}
