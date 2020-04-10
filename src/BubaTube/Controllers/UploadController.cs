using BubaTube.ViewModels.UploadVideoViewModel;
using Contracts.Data.DTO;
using Contracts.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Contracts.Write;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class UploadController : Controller
    {
        private readonly IVideoCommands videoWriteService;
        private readonly Func<string, string, PathInfo> getUploadPath;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<User> userManager;

        public UploadController(
            IVideoCommands videoWriteService,
            Func<string, string, PathInfo> getUploadPath,
            IWebHostEnvironment environment, 
            UserManager<User> userManager)
        {
            this.videoWriteService = videoWriteService;
            this.getUploadPath = getUploadPath;
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
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post(UploadVideoViewModel model)
        {
            int result = 0;
            if (ModelState.IsValid)
            {
                model.Categories = model.Categories[0]
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .ToArray();

                var pathInfo = this.getUploadPath(
                    this.environment.WebRootPath, 
                    this.ExtractFileExtension(model.Video.FileName));

                var dto = new VideoDTO()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Path = pathInfo.Path,
                    Url = pathInfo.FileName,
                    AuthorUserId = this.userManager.GetUserId(HttpContext.User),
                    Categories = model.Categories
                };

                result = await this.videoWriteService.Save(dto, model.Video);
            }

            return result > 0 
                ? this.Ok() 
                : this.StatusCode(500);
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

        private string ExtractFileExtension(string fileName)
        {
            return $".{fileName.Split('.').Last()}";
        }
    }
}