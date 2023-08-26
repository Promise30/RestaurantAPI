using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class ReviewSubmission
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
