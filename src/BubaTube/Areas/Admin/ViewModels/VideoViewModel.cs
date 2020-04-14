namespace BubaTube.Areas.Admin.ViewModels
{
    public class VideoViewModel
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public double Likes { get; set; }
    }
}
