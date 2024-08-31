using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
