using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
