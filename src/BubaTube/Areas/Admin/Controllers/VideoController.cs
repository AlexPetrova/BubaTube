﻿using BubaTube.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;
using Services.Contracts.Write;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/[action]")]
    public class VideosController : Controller
    {
        private IVideoQueries videoQueries;
        private IVideoCommands videoCommands;

        public VideosController(
            IVideoQueries videoQueries,
            IVideoCommands videoCommands)
        {
            this.videoQueries = videoQueries;
            this.videoCommands = videoCommands;
        }

        [HttpGet]
        public IActionResult ManageVideos()
        {
            IReadOnlyCollection<Video> models =
                this.videoQueries.GetAllForApproval()
                    .Select(video => new Video
                    {
                        Id = video.Id,
                        Title = video.Title,
                        Description = video.Description,
                        Likes = video.Likes,
                        Url = video.Url
                    })
                    .ToList();

            return View(models);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.videoCommands.Delete(id);

            return base.Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var result = await this.videoCommands.Approve(id);

            return base.Ok(result);
        }
    }
}