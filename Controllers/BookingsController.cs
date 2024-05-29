using Microsoft.AspNetCore.Mvc;
using CampingAPI.Models;
using CampingAPI.Data;
using LiteDB;

namespace CampingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private LiteDbContext _context = new LiteDbContext();

        [HttpGet("byUser/{userId}")]
        public IActionResult GetBookingsByUser(int userId)
        {
            var bookings = _context.Database.GetCollection<Booking>("Bookings").Find(x => x.UserId == userId);
            return Ok(bookings);
        }

        [HttpPost]
        public IActionResult CreateBooking([FromBody] Booking booking)
        {
            var spots = _context.Database.GetCollection<CampingSpot>("CampingSpots");
            var spot = spots.FindById(booking.CampingSpotId);

            
            if (spot == null || !spot.IsAvailable)
            {
                return BadRequest("Camping spot is not available.");
            }

           
            spot.IsAvailable = false;
            spots.Update(spot);

            var bookings = _context.Database.GetCollection<Booking>("Bookings");
            bookings.Insert(booking);
            return CreatedAtAction(nameof(CreateBooking), new { id = booking.Id }, booking);
        }

    }
}
