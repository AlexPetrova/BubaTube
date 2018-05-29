using BubaTube.ViewModels.UploadVideoViewModel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class UploadController : Controller
    {
        //service to process the path and map the dto to the db model
        public UploadController()
        {
        }

        public IActionResult UploadVideo()
        {
            return this.PartialView("_UploadVideo");
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(UploadVideoViewModel model)
        {
            var path = Path.GetRandomFileName();

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await model.Video.CopyToAsync(stream);
            }
            return Ok();
        }
    }
}
