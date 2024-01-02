using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace prjTravel.Controllers
{
    public class UserController : Controller
    {
        [Authorize(Roles = "Admin,Manager,User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
