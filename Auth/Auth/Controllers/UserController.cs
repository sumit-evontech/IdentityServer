using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
