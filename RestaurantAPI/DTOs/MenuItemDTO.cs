using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class MenuItemDTO
    {
        [DataType(DataType.ImageUrl)]
        public string MenuItemImageURL { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

    }
}
