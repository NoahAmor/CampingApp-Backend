namespace CampingAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CampingSpotId { get; set; }
        public DateTime Date { get; set; }
    }
}
