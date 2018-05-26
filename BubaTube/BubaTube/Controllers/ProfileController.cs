﻿using BubaTube.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class ProfileController : Controller
    {
        private UserManager<User> userManager;

        public ProfileController(UserManager<User> um)
        {
            this.userManager = um;
        }

        public IActionResult ProfileHomePage()
        {
            return this.View();
        }

        public async Task<IActionResult> Owner()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            

            return PartialView("_Owner");
        }
    }
}