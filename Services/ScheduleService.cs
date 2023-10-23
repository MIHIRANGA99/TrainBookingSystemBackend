using MongoDB.Driver;
using TrainBookingBackend.Models;
using TrainBookingBackend.Models.Interfaces;
using TrainBookingBackend.Services.Interfaces;

namespace TrainBookingBackend.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMongoCollection<Schedule> _schedules;
        public ScheduleService(IScheduleDBSettings dbSettings, IMongoClient mongoClient)
        {
            var databaseName = dbSettings.ScheduleDatabaseName;
            var collectionName = dbSettings.ScheduleCollectionName;

            var database = mongoClient.GetDatabase(databaseName);
            _schedules = database.GetCollection<Schedule>(collectionName);
        }
        public Schedule CreateSchedule(Schedule schedule)
        {
            _schedules.InsertOne(schedule);
            return schedule;
        }

        public void DeleteSchedule(string id)
        {
            _schedules.DeleteOne(schedule => schedule.Id == id);
        }

        public Schedule GetSchedule(string id)
        {
            return _schedules.Find(schedule => schedule.Id == id).FirstOrDefault<Schedule>();
        }

        public List<Schedule> GetSchedules()
        {
            return _schedules.Find(schedule => true).ToList();
        }

        public Schedule UpdateSchedule(string id, Schedule schedule)
        {
            _schedules.ReplaceOne(schedule => schedule.Id == id, schedule);
            return _schedules.Find(schedule => schedule.Id == id).FirstOrDefault<Schedule>();
        }
    }
}
