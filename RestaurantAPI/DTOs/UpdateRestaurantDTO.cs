using RestaurantAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class UpdateRestaurantDTO
    {

        [Required]
        public string RestaurantName { get; set; }
        [DataType(DataType.ImageUrl)]
        public string? RestaurantImageUrl { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Url)]
        public string? WebsiteUrl { get; set; }
        public string? StreetName { get; set; }

        [Required]
        public string Location { get; set; }
        [Required]
        public string? OperationDetails { get; set; }
        public string? ServiceType { get; set; }

        public ICollection<string>? Cuisines { get; set; }
        public bool IsOpen { get; set; } = true;
        public double OverallRating { get; set; }
        public int TotalReviews { get; set; }
        public Delivery? Delivery { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<MenuType> MenuTypes { get; set; }
        public int LocalGovernmentId { get; set; }
        public int StateId { get; set; }

        public virtual LocalGovernment LocalGovernment { get; set; }

        public virtual State State { get; set; }
    }
}
