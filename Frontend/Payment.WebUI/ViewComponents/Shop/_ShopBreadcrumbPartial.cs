using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.About
{
    public class _ShopBreadcrumbPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
