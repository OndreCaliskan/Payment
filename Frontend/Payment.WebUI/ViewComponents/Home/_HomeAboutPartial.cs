﻿using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.ViewComponents.Home
{
    public class _HomeAboutPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
