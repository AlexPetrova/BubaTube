using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Get;

namespace BubaTube.Areas.Admin.Controllers
{
    [Route("[controller]/[action]/{id?}")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IUserQueries userQueries;

        public UsersController(IUserQueries userQueries)
        {
            this.userQueries = userQueries;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ManageUsers()
        {
            return View("ManageUsers");
        }
    }
}
