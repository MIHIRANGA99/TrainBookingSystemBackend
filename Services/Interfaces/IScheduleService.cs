using TrainBookingBackend.Models;

namespace TrainBookingBackend.Services.Interfaces
{
    public interface IScheduleService
    {
        List<Schedule> GetSchedules();
        Schedule GetSchedule(string id);
        Schedule CreateSchedule(Schedule schedule);
        Schedule UpdateSchedule(string id, Schedule schedule);
        void DeleteSchedule(string id);
    }
}
