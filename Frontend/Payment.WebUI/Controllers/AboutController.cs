using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
