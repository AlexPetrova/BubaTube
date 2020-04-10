using BubaTube.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;
using System.Collections.Generic;
using System.Linq;

namespace BubaTube.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/[action]")]
    public class VideosController : Controller
    {
        private IVideoQueries videoQueries;

        public VideosController(IVideoQueries videoQueries)
        {
            this.videoQueries = videoQueries;
        }

        [HttpGet]
        public IActionResult ManageVideos()
        {
            IReadOnlyCollection<Video> models = 
                this.videoQueries.GetAllForApproval()
                    .Select(video => new Video
                    {
                        Title = video.Title,
                        Description = video.Description,
                        Likes = video.Likes,
                        Url = video.Url
                    })
                    .ToList();

            return View(models);
        }
    }
}