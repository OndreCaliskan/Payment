using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Contact
{
    public class _ContactPageTitlePartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
