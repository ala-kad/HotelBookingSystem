using HotelBookingSystem.Models;

namespace HotelBookingSystem.Services
{
   public class PricingService
    {
        private readonly List<Room> _rooms;
        private readonly List<Seasonality> _seasonalityFactors;
        private readonly List<OccupancyRate> _occupancyRates;

        public PricingService()
        {
            // Initialize data (In a real-world application, these would likely come from a database or an external service)
            _rooms = new List<Room>
            {
                new Room { RoomType = "Standard", BasePrice = 100 },
                new Room { RoomType = "Deluxe", BasePrice = 150 },
                new Room { RoomType = "Suite", BasePrice = 250 }
            };

            _seasonalityFactors = new List<Seasonality>
            {
                new Seasonality { Season = "Off-Season", AdjustmentFactor = -0.20m },
                new Seasonality { Season = "Peak Season", AdjustmentFactor = 0.30m }
            };

            _occupancyRates = new List<OccupancyRate>
            {
                new OccupancyRate { OccupancyLevel = "Low", AdjustmentFactor = -0.10m },
                new OccupancyRate { OccupancyLevel = "Medium", AdjustmentFactor = 0.0m },
                new OccupancyRate { OccupancyLevel = "High", AdjustmentFactor = 0.20m }
            };
        }

        public decimal GetBasePrice(string roomType)
        {
            var room = _rooms.FirstOrDefault(r => r.RoomType == roomType);
            if (room == null)
            {
                throw new ArgumentException($"Room type {roomType} not found.");
            }
            return room.BasePrice;
        }

        public decimal GetSeasonalityFactor(string season)
        {
            var seasonality = _seasonalityFactors.FirstOrDefault(s => s.Season == season);
            if (seasonality == null)
            {
                throw new ArgumentException($"Season {season} not found.");
            }
            return seasonality.AdjustmentFactor;
        }

        public decimal GetOccupancyFactor(int occupancyRate)
        {
            if (occupancyRate < 0 || occupancyRate > 100)
            {
                throw new ArgumentException("Occupancy rate must be between 0 and 100.");
            }

            if (occupancyRate <= 30) return -0.10m;
            if (occupancyRate <= 70) return 0.0m;
            return 0.20m;
        }

        public decimal GetCompetitorFactor(List<decimal> competitorPrices)
        {
            if (competitorPrices == null || competitorPrices.Count == 0)
            {
                return 0.0m;
            }

            var averageCompetitorPrice = competitorPrices.Average();
            return (averageCompetitorPrice - _rooms.Average(r => r.BasePrice)) / averageCompetitorPrice * 0.10m;
        }

        public decimal CalculateAdjustedPrice(string roomType, string season, int occupancyRate, List<decimal> competitorPrices)
        {
            var basePrice = GetBasePrice(roomType);
            var seasonalityFactor = GetSeasonalityFactor(season);
            var occupancyFactor = GetOccupancyFactor(occupancyRate);
            var competitorFactor = GetCompetitorFactor(competitorPrices);

            decimal adjustedPrice = basePrice * (1 + seasonalityFactor + occupancyFactor + competitorFactor);
            return Math.Round(adjustedPrice, 2);  // Round to 2 decimal places for currency
        }

    } 
}


