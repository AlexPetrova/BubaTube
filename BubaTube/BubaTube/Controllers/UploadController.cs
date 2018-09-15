using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Helpers.Contracts;
using BubaTube.Services.Contracts;
using BubaTube.ViewModels.UploadVideoViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class UploadController : Controller
    {
        private IUploadVideoService uploadVideoService;
        private IUploadVideoHelper uploadVideoHelper;
        private IHostingEnvironment environment;
        private UserManager<User> userManager;

        public UploadController
            (IUploadVideoService uploadVideoService,
            IUploadVideoHelper uploadVideoHelper,
            IHostingEnvironment environment, 
            UserManager<User> userManager)
        {
            this.uploadVideoService = uploadVideoService;
            this.uploadVideoHelper = uploadVideoHelper;
            this.environment = environment;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult UploadVideo()
        {
            return this.PartialView("_UploadVideo");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(100000000)]
        public async Task<IActionResult> Post(UploadVideoViewModel model)
        {
            //not binding the js array 
            var str = model.Categories[0];
            model.Categories = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            
            var path = this.uploadVideoHelper.GeneratePath(this.environment.WebRootPath);

            if (ModelState.IsValid)
            {
                try
                {
                    await this.uploadVideoService.SaveToRootFolder(model.Video, path);
                }
                catch
                {
                    return this.StatusCode(500);
                }
            }

            var dto = new VideoDTO()
            {
                Title = model.Title,
                Description = model.Description,
                Path = path,
                AuthorId = this.userManager.GetUserId(HttpContext.User),
                Categories = model.Categories
            };

            this.uploadVideoService.SaveToDatabase(dto);

            return Ok();
        }

    }
}
