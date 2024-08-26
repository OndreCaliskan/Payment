using Microsoft.AspNetCore.Mvc;
using Payment.WebUI.Models.Mail;

namespace Payment.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}
