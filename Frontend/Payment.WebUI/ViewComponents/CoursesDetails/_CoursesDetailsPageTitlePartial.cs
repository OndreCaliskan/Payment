using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.CoursesDetails
{
    public class _CoursesDetailsPageTitlePartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
