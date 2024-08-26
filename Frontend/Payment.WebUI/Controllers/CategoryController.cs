using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.BusinessLayer.Abstract;
using Payment.BusinessLayer.Concrete;
using Payment.WebUI.DTOs.CategoryDtos;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
       



        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7066/api/Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
        {
            if (createCategoryDto.File == null || createCategoryDto.File.Length == 0)
            {
                ModelState.AddModelError("File", "Lütfen bir dosya yükleyin.");
                return View();
            }

            string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            string extension = Path.GetExtension(createCategoryDto.File.FileName).ToLower();

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
                createCategoryDto.File.CopyTo(stream);
            }

            createCategoryDto.ImagePath = "/img/" + filename;
            createCategoryDto.CreateTime = DateTime.Now;
            createCategoryDto.UpdateTime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View();
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/Category", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7066/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            updateCategoryDto.UpdateTime = DateTime.Now;

            if (updateCategoryDto.File != null && updateCategoryDto.File.Length > 0)
            {
                // Dosya uzantısını kontrol etme
                string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                string extension = Path.GetExtension(updateCategoryDto.File.FileName).ToLower();

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("File", "Sadece resim dosyaları yüklenebilir.");
                    return View(updateCategoryDto);
                }

                // Mevcut resmi silme işlemi (eğer varsa)
                if (!string.IsNullOrEmpty(updateCategoryDto.ImagePath))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", updateCategoryDto.ImagePath.TrimStart('/'));
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
                    updateCategoryDto.File.CopyTo(stream);
                }

                updateCategoryDto.ImagePath = "/img/" + filename;
            }

            if (!ModelState.IsValid)
            {
                return View(updateCategoryDto);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/Category", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View(updateCategoryDto);
        }
    }
}
