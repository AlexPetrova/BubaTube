
using BubaTube.Areas.Admin.Servises.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BubaTube.Areas.Admin.Controllers
{
    public class ControlUsersController : Controller
    {
        private readonly IManageUsersService controlUsersService;

        public ControlUsersController(
            IManageUsersService controlUsersService)
        {
            this.controlUsersService = controlUsersService;
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
