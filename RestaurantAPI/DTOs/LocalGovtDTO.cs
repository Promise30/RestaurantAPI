using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class LocalGovtDTO
    {
        public int LocalGovernmentId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
