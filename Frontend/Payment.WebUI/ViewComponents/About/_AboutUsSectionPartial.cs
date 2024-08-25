using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.About
{
    public class _AboutUsSectionPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
