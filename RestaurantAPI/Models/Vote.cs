namespace RestaurantAPI.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public bool IsUpVote { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
