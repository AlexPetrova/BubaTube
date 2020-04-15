using Contracts.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BubaTube.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> userManager;

        public ProfileController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult ProfileHomePage()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> Owner()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            
            return PartialView("_Owner");
        }
    }
}
