namespace BubaTube.Areas.Admin.Models
{
    public class Video
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public double Likes { get; set; }
    }
}
