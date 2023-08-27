using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        [Required]
        public string RestaurantName { get; set; } = string.Empty;
        [DataType(DataType.ImageUrl)]
        public string? RestaurantImageUrl { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
        [DataType(DataType.Url)]
        public string? WebsiteUrl { get; set; }
        public string? StreetName { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public string? OperationDetails { get; set; }
        public string? ServiceType { get; set; }
        public string? Cuisines { get; set; }
        public bool IsOpen { get; set; } = true;
        public double OverallRating { get; set; }
        public int TotalReviews { get; set; }
        public Delivery? Delivery { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<MenuType> MenuTypes { get; set; } = new List<MenuType>();
        public int LocalGovernmentId { get; set; }
        public virtual LocalGovernment? LocalGovernment { get; set; }
    }
}
