using Contracts.Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;
using System.Collections.Generic;

namespace BubaTube.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]")]
    public class UsersController : Controller
    {
        private IUserQueries userQueries;

        public UsersController(IUserQueries userQueries)
        {
            this.userQueries = userQueries;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ManageUsers()
        {
            return base.View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{months}")]
        public IActionResult Filter(int months)
        {
            IReadOnlyCollection<UserDTO> users = this.userQueries.ByLastActivity(months);
            
            return base.Ok(users);
        }
    }
}
