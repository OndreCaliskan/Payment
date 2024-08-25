using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.CoursesDetails
{
    public class _CoursesDetailsCoursePartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

