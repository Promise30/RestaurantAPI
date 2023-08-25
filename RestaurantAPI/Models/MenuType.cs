using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class MenuType
    {
        [Key]
        public int MenuTypeId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        public string Name { get; set; }
        public int RestaurantId { get; set; }
        public virtual ICollection<MenuItem>? MenuItems { get; set; } = new List<MenuItem>();
        public virtual Restaurant Restaurant { get; set; }

    }
}
