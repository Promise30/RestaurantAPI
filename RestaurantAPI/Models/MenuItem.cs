using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        [DataType(DataType.ImageUrl)]
        public string MenuItemImageURL { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be more than 100 characters.")]
        public string Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public int MenuTypeId { get; set; }
        public virtual MenuType MenuType { get; set; }
    }
}
