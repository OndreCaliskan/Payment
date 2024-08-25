using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Layout
{
    public class _LayoutFooterPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    
    }
}
