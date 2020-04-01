using Contracts.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;
using System;
using System.Collections.Generic;

namespace BubaTube.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("[area]/[controller]/[action]")]
    public class UsersController : Controller
    {
        private IUserQueries userQueries;

        public UsersController(IUserQueries userQueries)
        {
            this.userQueries = userQueries;
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            return base.View();
        }

        [HttpGet]
        public IActionResult FilterByLastActivity()
        {
            return base.PartialView("_FilterByLastActivity");
        }

        [HttpGet]
        public IActionResult FilterByPeriod()
        {
            return base.PartialView("_FilterByPeriod");
        }

        [HttpGet("{months}")]
        public IActionResult FilterByLastActivity(int months)
        {
            IReadOnlyCollection<UserDTO> users = this.userQueries.ByLastActivity(months);

            return base.PartialView("_FilterResult", users);
        }

        [HttpGet("{from}/{to}")]
        public IActionResult FilterPerPeriod(DateTime from, DateTime to)
        {
            IReadOnlyCollection<UserDTO> users = this.userQueries.RegisterdInPeriod(from, to);

            return base.PartialView("_FilterResult", users);
        }
    }
}
