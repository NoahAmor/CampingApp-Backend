using Microsoft.AspNetCore.Mvc;
using CampingAPI.Models;
using CampingAPI.Data;
using LiteDB;

namespace CampingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private LiteDbContext _context = new LiteDbContext();

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Database.GetCollection<User>("Users").FindAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Database.GetCollection<User>("Users").FindById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var users = _context.Database.GetCollection<User>("Users");
            users.Insert(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin loginDetails)
        {
            var users = _context.Database.GetCollection<User>("Users");
            var user = users.FindOne(x => x.Username == loginDetails.Username && x.Password == loginDetails.Password);
            if (user == null) return Unauthorized("Invalid username or password");
            return Ok(user);
        }

        [HttpPut("{id}")]
        
        public IActionResult UpdateUser(int id, [FromBody] User userUpdate)
        {
            var users = _context.Database.GetCollection<User>("Users");
            var user = users.FindById(id);
            if (user == null) return NotFound();

            
            user.Username = userUpdate.Username;
            user.Email = userUpdate.Email;
            user.Password = userUpdate.Password;

            users.Update(user);
            return Ok(user);
        }

    }
}
