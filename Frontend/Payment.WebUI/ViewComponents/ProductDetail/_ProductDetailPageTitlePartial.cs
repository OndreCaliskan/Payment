using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.ProductDetail
{
    public class _ProductDetailPageTitlePartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
