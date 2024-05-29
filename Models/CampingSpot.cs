namespace CampingAPI.Models
{
    public class CampingSpot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }

        public int OwnerId { get; set; }
    }
}
