using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public string? ReviewerName { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        public int UpVoteCount { get; set; }
        public int DownVoteCount { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 - 5")]
        public int Rating { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public virtual ICollection<Vote>? Votes { get; set; }
    }
}
