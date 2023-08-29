using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class CreateMenuItem
    {

        public string? MenuItemImageURL { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public decimal Price { get; set; }
    }
}