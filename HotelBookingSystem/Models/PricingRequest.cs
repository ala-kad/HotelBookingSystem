namespace HotelBookingSystem.Models
{
    public class PricingRequest
    {
        public string RoomType { get; set; }
        public string Season { get; set; }
        public int OccupancyRate { get; set; }
        public List<decimal> CompetitorPrices { get; set; }
    }
}