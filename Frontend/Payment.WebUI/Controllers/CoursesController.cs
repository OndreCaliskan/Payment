using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CoursesDetails()
        {
            return View();
        }
    }
}
