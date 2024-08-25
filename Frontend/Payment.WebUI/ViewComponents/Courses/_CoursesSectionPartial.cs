using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Courses
{
    public class _CoursesSectionPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
