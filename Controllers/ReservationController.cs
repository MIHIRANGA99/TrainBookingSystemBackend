using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainBookingBackend.Models;
using TrainBookingBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainBookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly IConfiguration configuration;

        public ReservationController(IReservationService reservationService, IUserService userService, IConfiguration configuration)
        {
            _reservationService = reservationService;
            _userService = userService;
            this.configuration = configuration;
        }

        // GET: api/<ReservationController>
        [HttpGet(Name = "Get All Reservations"), Authorize]
        public ActionResult<List<Reservation>> Get()
        {
            return _reservationService.GetReservations();
        }

        // GET api/<ReservationController>/5
        [HttpGet("{id}"), Authorize]
        public ActionResult<Reservation> Get(string id)
        {
            return _reservationService.GetReservation(id);
        }

        // POST api/<ReservationController>
        [HttpPost(Name = "Create New Reservation")]
        public ActionResult Post([FromBody] Reservation reservation)
        {
            _reservationService.CreateReservation(reservation);

            var user = _userService.GetUser(reservation.UserId);
            user.Reservations.Add(reservation.Id);

            _userService.UpdateUser(reservation.UserId, user);

            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
        }

        // PUT api/<ReservationController>/5
        [HttpPut("{id}"), Authorize]
        public ActionResult Put(string id, [FromBody] Reservation reservation)
        {
            var existingReservation = _reservationService.GetReservation(id);

            if (existingReservation == null)
            {
                return NotFound($"Reservation with ID: {id} Not Found!");
            }

            _reservationService.UpdateReservation(id, reservation);
            return CreatedAtAction(nameof(Put), new { id = reservation.Id }, reservation);
        }

        // DELETE api/<ReservationController>/5
        [HttpDelete("{id}"), Authorize]
        public ActionResult Delete(string id)
        {
            var existingReservation = _reservationService.GetReservation(id);

            if (existingReservation == null)
            {
                return NotFound($"Reservation with ID: {id} Not Found!");
            }

            _reservationService.DeleteReservation(id);
            return Ok($"reservation with ID: {id} has successfully deleted!");
        }
    }
}
