using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Index
{
    public class _HomeHeroPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
