using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Write;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Controllers
{
    public class ControlUsersController : Controller
    {
        private readonly IUserCommands userCommands;

        public ControlUsersController(
            IUserCommands userCommands)
        {
            this.userCommands = userCommands;
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]")]
        public IActionResult ManageUsers()
        {
            return View();
        }

        [Area("Admin")]
        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]")]
        public IActionResult GetUsersByLastActivity(int months)
        {
            return View();
        }
    }
}
