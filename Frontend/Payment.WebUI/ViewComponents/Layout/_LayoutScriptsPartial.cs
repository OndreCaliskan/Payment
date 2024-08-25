using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Layout
{
    public class _LayoutScriptsPartial :  ViewComponent
    {
        public IViewComponentResult Invoke()
    {
        return View();
    }
    
    }
}
