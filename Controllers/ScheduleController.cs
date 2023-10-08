using Microsoft.AspNetCore.Mvc;
using TrainBookingBackend.Models;
using TrainBookingBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainBookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly ITrainService _trainService;
        private readonly IConfiguration configuration;

        public ScheduleController(IScheduleService scheduleService, IConfiguration configuration, ITrainService trainService)
        {
            this.configuration = configuration;
            _scheduleService = scheduleService;
            _trainService = trainService;
        }

        // GET: api/<ScheduleController>
        [HttpGet]
        public ActionResult<List<Schedule>> Get()
        {
            return _scheduleService.GetSchedules();
        }

        // GET api/<ScheduleController>/5
        [HttpGet("{id}")]
        public ActionResult<Schedule> Get(string id)
        {
            return _scheduleService.GetSchedule(id);
        }

        // POST api/<ScheduleController>
        [HttpPost]
        public ActionResult Post([FromBody] Schedule schedule)
        {
            _scheduleService.CreateSchedule(schedule);

            var train = _trainService.GetTrain(schedule.TrainId);

            if (train == null)
            {
                return NotFound($"Train with ID: {schedule.TrainId} not found!");
            }

            train.Schedules.Add(schedule.Id);
            _trainService.UpdateTrain(schedule.TrainId, train);

            return CreatedAtAction(nameof(Get), new { id = schedule.Id }, schedule);
        }

        // PUT api/<ScheduleController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Schedule schedule)
        {
            var existingSchedule = _scheduleService.GetSchedule(id);

            if (existingSchedule == null)
            {
                return NotFound($"Schedule with ID: {id} Not Found!");
            }

            _scheduleService.UpdateSchedule(id, schedule);
            return CreatedAtAction(nameof(Put), new { id = schedule.Id }, schedule);
        }

        // DELETE api/<ScheduleController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existingSchedule = _scheduleService.GetSchedule(id);

            if (existingSchedule == null)
            {
                return NotFound($"Schedule with ID: {id} Not Found!");
            }

            _scheduleService.DeleteSchedule(id);
            return Ok($"Schedule with ID: {id} has successfully deleted!");
        }
    }
}
