using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
