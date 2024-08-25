using Microsoft.AspNetCore.Mvc;

namespace Payment.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View();
        }
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string addCategoryViewModel)
        {

            return View();
        }


        public async Task<IActionResult> DeleteCategory(int id)
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {

            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdateCategory(string updateCategoryViewModel)
        {


            return View();
        }
    }
}
