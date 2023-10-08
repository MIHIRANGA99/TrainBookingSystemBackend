using TrainBookingBackend.Models;

namespace TrainBookingBackend.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(string id);
        User CreateUser(User user);
        User? LoginUser(string username, string password);
        User UpdateUser(string id, User user);
        void DeleteUser(string id);
    }
}
