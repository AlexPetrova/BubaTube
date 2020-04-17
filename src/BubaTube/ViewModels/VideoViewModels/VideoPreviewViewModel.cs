namespace BubaTube.ViewModels.VideoViewModels
{
    public class VideoPreviewViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public AuthorPreviewViewModel Author { get; set; }
    }
}
