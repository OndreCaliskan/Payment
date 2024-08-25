using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Courses
{
    public class _CoursesPageTitlePartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
