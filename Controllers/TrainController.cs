using Microsoft.AspNetCore.Mvc;
using TrainBookingBackend.Models;
using TrainBookingBackend.Services;
using TrainBookingBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainBookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;
        private readonly IConfiguration configuration;

        public TrainController(ITrainService trainService, IConfiguration configuration)
        {
            this.configuration = configuration;
            _trainService = trainService;
        }

        // GET: api/<TrainController>
        [HttpGet]
        public ActionResult<List<Train>> Get()
        {
            return _trainService.GetTrains();
        }

        // GET api/<TrainController>/5
        [HttpGet("{id}")]
        public ActionResult<Train> Get(string id)
        {
            return _trainService.GetTrain(id);
        }

        // POST api/<TrainController>
        [HttpPost]
        public ActionResult Post([FromBody] Train train)
        {
            _trainService.CreateTrain(train);

            return CreatedAtAction(nameof(Get), new { id = train.Id }, train);
        }

        // PUT api/<TrainController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Train train)
        {
            var existingReservation = _trainService.GetTrain(id);

            if (existingReservation == null)
            {
                return NotFound($"Train with ID: {id} Not Found!");
            }

            _trainService.UpdateTrain(id, train);
            return CreatedAtAction(nameof(Put), new { id = train.Id }, train);
        }

        // DELETE api/<TrainController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existingReservation = _trainService.GetTrain(id);

            if (existingReservation == null)
            {
                return NotFound($"Train with ID: {id} Not Found!");
            }

            _trainService.DeleteTrain(id);
            return Ok($"Train with ID: {id} has successfully deleted!");
        }
    }
}
