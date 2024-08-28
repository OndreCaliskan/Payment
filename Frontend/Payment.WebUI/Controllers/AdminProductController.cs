using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Payment.BusinessLayer.Abstract;
using Payment.BusinessLayer.Concrete;
using Payment.DataAccessLayer.Concrete;
using Payment.DtoLayer.Dtos.CategoryDtos;
using Payment.EntityLayer.Concrete;
using Payment.WebUI.DTOs.CategoryDtos;
using Payment.WebUI.DTOs.ProductDtos;
using Payment.WebUI.Models;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class AdminProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;


        public AdminProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task<IActionResult> Index()
        {
            using (var context = new Context())
            {
                ViewBag.CategoryName = context.Categories.ToDictionary(c => c.Id, c => c.Name);
                
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7066/api/Product");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            // Context yerine dependency injection kullanarak veri erişimi yapın
            using (var context = new Context())
            {
                var categoryNames = context.Categories // Kategoriler tablosunu kullanın
                    .Select(x => new
                    {
                        x.Id,
                        x.Name
                    })
                    .ToList();

                // Kategori isimlerini ve Id'lerini ViewBag'e aktarıyoruz
                ViewBag.Categories = categoryNames;
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductDto model)
        {
            using (var context = new Context())
            {
                var categoryNames = context.Categories 
                    .Select(x => new
                    {
                        x.Id,
                        x.Name
                    })
                    .ToList();

       
                ViewBag.Categories = categoryNames;
            }
            if (model.File == null || model.File.Length == 0)
            {
                ModelState.AddModelError("File", "Lütfen bir dosya yükleyin.");
                return View();
            }

            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(model.File.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("File", "Sadece resim dosyaları yüklenebilir.");
                return View();
            }


            // Dosya kaydetme işlemi
            string filename = Guid.NewGuid().ToString() + extension;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                model.File.CopyTo(stream);
            }

            model.CoverImage = "/img/" + filename;
            model.CreateTime = DateTime.Now;
            model.UpdateTime = DateTime.Now;



            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/Product/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7066/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {

            using (var context = new Context())
            {
                var categoryNames = context.Categories // Kategoriler tablosunu kullanın
                    .Select(x => new
                    {
                        x.Id,
                        x.Name
                    })
                    .ToList();

                // Kategori isimlerini ve Id'lerini ViewBag'e aktarıyoruz
                ViewBag.Categories = categoryNames;
            }
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Product/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(values);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto model)
        {

            using (var context = new Context())
            {
                var categoryNames = context.Categories
                    .Select(x => new
                    {
                        x.Id,
                        x.Name
                    })
                    .ToList();

                ViewBag.Categories = categoryNames;
            }

            if (model.File == null || model.File.Length == 0)
            {
                ModelState.AddModelError("File", "Lütfen bir dosya yükleyin.");
                return View();
            }
            if (model.File != null && model.File.Length > 0)
            {
                // Dosya uzantısını kontrol etme
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                string extension = Path.GetExtension(model.File.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("File", "Sadece resim dosyaları yüklenebilir.");
                    return View(model);
                }

                // Mevcut resmi silme işlemi (eğer varsa)
                if (!string.IsNullOrEmpty(model.CoverImage))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", model.CoverImage.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Yeni dosyayı kaydetme
                string filename = Guid.NewGuid().ToString() + extension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", filename);

                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                model.CoverImage = "/img/" + filename;
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.UpdateTime = DateTime.Now;
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/Product/", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> IsActiveApproved(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Product/IsActiveAproved?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        public async Task<IActionResult> IsActiveApprovedCancel(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Product/IsActiveAprovedCancel?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
