using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string? ReviewerName { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 - 5")]
        public int Rating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
    }
}
