using MongoDB.Driver;
using TrainBookingBackend.Models;
using TrainBookingBackend.Models.Interfaces;
using TrainBookingBackend.Services.Interfaces;

namespace TrainBookingBackend.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public ReservationService(ISystemDBSettings systemDBSettings, IMongoClient mongoClient)
        {
            var databaseName = systemDBSettings.DatabaseName;
            var collectionName = systemDBSettings.CollectionName;

            var database = mongoClient.GetDatabase(databaseName);
            _reservations = database.GetCollection<Reservation>(collectionName);
        }

        public Reservation CreateReservation(Reservation reservation)
        {
            _reservations.InsertOne(reservation);
            return reservation;
        }

        public void DeleteReservation(string id)
        {
            _reservations.DeleteOne(id);
        }

        public Reservation GetReservation(string id)
        {
            return _reservations.Find(reservation => reservation.Id == id).FirstOrDefault<Reservation>();
        }

        public List<Reservation> GetReservations()
        {
            return _reservations.Find(reservation => true).ToList();
        }

        public Reservation UpdateReservation(string id, Reservation reservation)
        {
            _reservations.ReplaceOne(reservation => reservation.Id == id, reservation);
            return _reservations.Find(reservation => reservation.Id == id).FirstOrDefault<Reservation>();
        }
    }
}
