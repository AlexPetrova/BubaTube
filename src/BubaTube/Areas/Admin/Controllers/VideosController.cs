using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;

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
            return View();
        }
    }
}