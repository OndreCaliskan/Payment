using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.AboutUs
{
    public class _AboutUsBreadCrumbPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
