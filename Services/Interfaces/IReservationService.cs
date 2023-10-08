using TrainBookingBackend.Models;

namespace TrainBookingBackend.Services.Interfaces
{
    public interface IReservationService
    {
        List<Reservation> GetReservations();
        Reservation GetReservation(string id);
        Reservation CreateReservation(Reservation reservation);
        Reservation UpdateReservation(string id, Reservation reservation);
        void DeleteReservation(string id);
    }
}
