using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Home
{
    public class _HomeTrainersPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();

        }
    }
}
