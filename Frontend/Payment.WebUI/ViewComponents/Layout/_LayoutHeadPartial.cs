using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Layout
{
    public class _LayoutHeadPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

