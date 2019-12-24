namespace BubaTube.Data.Models
{
    public class UserVideo
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int VideoId { get; set; }

        public Video Video { get; set; }
    }
}
