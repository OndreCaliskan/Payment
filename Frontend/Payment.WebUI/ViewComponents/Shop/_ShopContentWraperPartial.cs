using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.About
{
    public class _ShopContentWraperPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
