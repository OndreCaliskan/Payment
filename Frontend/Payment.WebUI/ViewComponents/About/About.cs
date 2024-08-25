using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.About
{
    public class About : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
