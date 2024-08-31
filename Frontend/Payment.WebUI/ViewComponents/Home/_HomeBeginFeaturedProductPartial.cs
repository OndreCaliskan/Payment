using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Home
{
    public class _HomeBeginFeaturedProductPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
