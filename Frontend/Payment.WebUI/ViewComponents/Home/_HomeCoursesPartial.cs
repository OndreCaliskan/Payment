using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Home
{
    public class _HomeCoursesPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();

        }
    }
}
