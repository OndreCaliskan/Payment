using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
