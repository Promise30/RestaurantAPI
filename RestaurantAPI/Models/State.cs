using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<LocalGovernment> LocalGovernments { get; set; }

    }
}
