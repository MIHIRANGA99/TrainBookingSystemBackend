using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TrainBookingBackend.Models;
using TrainBookingBackend.Models.Dtos;
using TrainBookingBackend.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TrainBookingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            this.configuration = configuration;
        }

        // GET: api/<UserController>
        [HttpGet(Name ="Get All Users"), Authorize]
        public ActionResult<List<User>> Get()
        {
            return _userService.GetUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            return _userService.GetUser(id);
        }

        // POST api/<UserController>
        [HttpPost("register")]
        public ActionResult Post([FromBody] User user)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = passwordHash;
            _userService.CreateUser(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public ActionResult<User> Post([FromBody] UserLoginDto loginDto)
        {
            var exsistingUser = _userService.LoginUser(loginDto.Email, loginDto.Password);

            if (exsistingUser != null)
            {
                var token = CreateToken(exsistingUser);
                return Ok(token);
            }

            return NotFound("User Not Found!");
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] User user)
        {
            var existingUser = _userService.GetUser(id);
            if (existingUser == null)
            {
                return NotFound($"User with NIC: {id} has not found!");
            }

            _userService.UpdateUser(id, user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var existingUser = _userService.GetUser(id);
            if (existingUser != null)
            {
                NotFound($"User with NIC: {id} has not found!");
            }

            _userService.DeleteUser(id);
            return Ok($"user with NIC: {id} successfully deleted!");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(2), signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
