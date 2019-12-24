using BubaTube.Helpers.Contracts;
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
        private readonly IVideoCommands videoWriteService;
        private readonly ICategoryCommands categorySaverService;
        private readonly IUploadVideoHelper uploadVideoHelper;
        private readonly IHostingEnvironment environment;
        private readonly UserManager<User> userManager;

        public UploadController
            (IVideoCommands videoWriteService,
            ICategoryCommands categorySaverService,
            IUploadVideoHelper uploadVideoHelper,
            IHostingEnvironment environment, 
            UserManager<User> userManager)
        {
            this.videoWriteService = videoWriteService;
            this.categorySaverService = categorySaverService;
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
        public async Task<IActionResult> Post(UploadVideoViewModel   model)
        {
            int result = 0;
            if (ModelState.IsValid)
            {
                var str = model.Categories[0];
                model.Categories = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                var path = this.uploadVideoHelper.GeneratePath(this.environment.WebRootPath);
                var dto = new VideoDTO()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Path = path,
                    AuthorUserName = this.userManager.GetUserId(HttpContext.User),
                    Categories = model.Categories
                };

                this.categorySaverService.SaveToDatabase(dto.Categories);
                result = await this.videoWriteService.Save(dto, model.Video, path);
                
            }
            return result == 0 ? Ok() : this.StatusCode(500);
        }

        [Authorize]
        public IActionResult ErrorResponse()
        {
            return this.PartialView("_ErrorResponse");
        }

        [Authorize]
        public IActionResult SuccessResponse()
        {
            return this.PartialView("_SuccessResponse");
        }
    }
}