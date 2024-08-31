using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Layout
{
    public class _LayoutHeaderPartial : ViewComponent
    { 
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
