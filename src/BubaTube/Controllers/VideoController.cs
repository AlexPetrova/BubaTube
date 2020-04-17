using BubaTube.ViewModels.VideoViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Controllers
{
    public class VideoController : Controller
    {
        private readonly IVideoQueries videoQueries;

        public VideoController(IVideoQueries videoQueries)
        {
            this.videoQueries = videoQueries;
        }

        public IActionResult MostRecent()
        {
            IReadOnlyCollection<VideoPreviewViewModel> videos 
                = this.videoQueries.MostResentVideos()
                    .Select(dto => new VideoPreviewViewModel
                    {
                        Id = dto.Id,
                        Title = dto.Title,
                        Url = $"video/{dto.FileName}",
                        Author= new AuthorPreviewViewModel
                        {
                            Id = dto.Author.Id,
                            FirstName = dto.Author.FirstName,
                            LastName = dto.Author.LastName,
                            ProgilePicture = dto.Author.ProfilePicture
                        }
                    })
                    .ToList();

            return base.View(videos);
        }
    }
}
