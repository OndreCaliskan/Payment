using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Index
{
    public class _HomeSliderWithCategoryPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
