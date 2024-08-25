using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Home
{
    public class _HomeCountsPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();

        }
    }
}