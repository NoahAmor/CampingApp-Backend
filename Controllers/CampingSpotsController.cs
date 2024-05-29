using Microsoft.AspNetCore.Mvc;
using CampingAPI.Models;
using CampingAPI.Data;
using LiteDB;

namespace CampingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampingSpotsController : ControllerBase
    {
        private LiteDbContext _context = new LiteDbContext();

        [HttpGet]
        public IActionResult GetAllSpots()
        {
            var spots = _context.Database.GetCollection<CampingSpot>("CampingSpots").FindAll();
            return Ok(spots);
        }

        [HttpGet("{id}")]
        public IActionResult GetCampingSpot(int id)
        {
            var spot = _context.Database.GetCollection<CampingSpot>("CampingSpots").FindById(id);
            if (spot == null)
            {
                return NotFound("Camping spot not found.");
            }
            return Ok(spot);
        }

        [HttpGet("byOwner/{ownerId}")]
        public IActionResult GetCampingSpotsByOwner(int ownerId)
        {
            var spots = _context.Database.GetCollection<CampingSpot>("CampingSpots")
                .Find(x => x.OwnerId == ownerId);
            return Ok(spots);
        }

        [HttpPost]
        public IActionResult AddSpot([FromBody] CampingSpot newSpot)
        {
            var spots = _context.Database.GetCollection<CampingSpot>("CampingSpots");
            spots.Insert(newSpot);
            return CreatedAtAction(nameof(GetAllSpots), new { id = newSpot.Id }, newSpot);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSpot(int id, [FromBody] CampingSpot spotUpdate)
        {
            var spots = _context.Database.GetCollection<CampingSpot>("CampingSpots");
            var spot = spots.FindById(id);
            if (spot == null) return NotFound();
            spot.Name = spotUpdate.Name;
            spot.Location = spotUpdate.Location;
            spot.IsAvailable = spotUpdate.IsAvailable;
            spot.OwnerId = spotUpdate.OwnerId;
            spots.Update(spot);
            return Ok(spot);
        }


    }
}
