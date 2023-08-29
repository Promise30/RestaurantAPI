using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class CreateMenuType
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        public string Name { get; set; } = string.Empty;


    }
}
