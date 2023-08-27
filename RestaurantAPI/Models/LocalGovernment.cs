using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class LocalGovernment
    {
        [Key]
        public int LocalGovernmentId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int StateId { get; set; }

        public virtual State State { get; set; }
        public List<Restaurant> Restaurants { get; } = new();

    }
}
