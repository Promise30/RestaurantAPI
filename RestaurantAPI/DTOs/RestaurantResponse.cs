using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class RestaurantResponse
    {
        public int RestaurantId { get; set; }
        public string? RestaurantImageUrl { get; set; }
        [Required]
        public string RestaurantName { get; set; } = string.Empty;
        public double OverallRating { get; set; }
        public string? ServiceType { get; set; }
        public string? StreetName { get; set; }
    }
}
