using BubaTube.Data.DTO;
using BubaTube.Data.Models;
using BubaTube.Helpers.Contracts;
using BubaTube.Services.Contracts;
using BubaTube.Services.Contracts.Write;
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
        private IVideoWriteService videoWriteService;
        private ICategoryWriteService categorySaverService;
        private IUploadVideoHelper uploadVideoHelper;
        private IHostingEnvironment environment;
        private UserManager<User> userManager;

        public UploadController
            (IVideoWriteService videoWriteService,
            ICategoryWriteService categorySaverService,
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
        public async Task<IActionResult> Post(UploadVideoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var str = model.Categories[0];
                model.Categories = str.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();

                var path = this.uploadVideoHelper.GeneratePath(this.environment.WebRootPath);

                try
                {
                    await this.videoWriteService.SaveToRootFolder(model.Video, path);
                }
                catch
                {
                    return this.StatusCode(500);
                }

                var dto = new VideoDTO()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Path = path,
                    AuthorId = this.userManager.GetUserId(HttpContext.User),
                    Categories = model.Categories
                };

                this.categorySaverService.SaveToDatabase(dto.Categories);
                this.videoWriteService.SaveToDatabase(dto);
            }
            
            return Ok();
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