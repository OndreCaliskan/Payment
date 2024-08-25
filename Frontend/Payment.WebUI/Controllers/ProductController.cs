using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            
            return View();
        }
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(string addProductViewModel )
        {
            
            return View();
        }


        public async Task<IActionResult> DeleteProduct(int id)
        {
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
           
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> UpdateProduct(string updateProductViewModel )
        {
            

            return View();
        }
    }
}
