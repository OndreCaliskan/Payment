using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.About
{
    public class _AboutCountsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
