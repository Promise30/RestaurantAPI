using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class RestaurantDTO
    {
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
        public int StateId { get; set; }
        public int LocalGovtId { get; set; }
        [Required]
        public string? OperationDetails { get; set; }
        public DeliveryDTO? Delivery { get; set; }
        public string? ServiceType { get; set; }
        public string? Cuisines { get; set; }

    }
}
