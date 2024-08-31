using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    [AllowAnonymous]

    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
