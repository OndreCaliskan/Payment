using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.CategoryDtos; // `CategoryDto` için gerekli namespace
using Payment.WebUI.Helpers;
using Payment.WebUI.Models;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Payment.DataAccessLayer.Concrete;
using Payment.BusinessLayer.Abstract; // DbContext için gerekli namespace

namespace Payment.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICategoryService _categoryService;
        


        public CategoryController(IHttpClientFactory httpClientFactory,ICategoryService categoryService) 
        {
            _httpClientFactory = httpClientFactory;
            _categoryService = categoryService;
           
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 5)
        {
            var client = _httpClientFactory.CreateClient();

            var responseMessage2 = await client.GetAsync("https://localhost:7066/api/User/");
            if (responseMessage2.IsSuccessStatusCode)
            {
                var user = await responseMessage2.Content.ReadFromJsonAsync<AppUser>();
                TempData["UserName"] = user.Name;

                using (var context = new Context())
                {
                    ViewBag.CategoryName = context.Categories.ToDictionary(c => c.Id, c => c.Name);
                }

                var categories = _categoryService.GetCategories(page, pageSize); // await yok
                var totalCategories = _categoryService.GetTotalCategories();

                var result = new CategoryListViewModel
                {
                    Categories = categories.ToList(), // `ToList` çağrısı ile tür uyumunu sağlama
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        TotalItems = totalCategories,
                        ItemsPerPage = pageSize,
                        TotalPages = (int)Math.Ceiling((decimal)totalCategories / pageSize)
                    }
                };

                return View(result);
            }

            return RedirectToAction("Index", "Login");
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
        {
           
                if (createCategoryDto.File != null)
                {
                    createCategoryDto.ImagePath = ImageHelper.SaveImage(createCategoryDto.File);
                }

                createCategoryDto.CreateTime = DateTime.Now;
                createCategoryDto.UpdateTime = DateTime.Now;
                createCategoryDto.CreateUser = TempData["UserName"].ToString();
                createCategoryDto.UpdateUser = TempData["UserName"].ToString();

                if (!ModelState.IsValid)
                {
                    return View(createCategoryDto);
                }

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createCategoryDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7066/api/Category", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(createCategoryDto);
           
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
            try
            {
                updateCategoryDto.UpdateTime = DateTime.Now;
                updateCategoryDto.UpdateUser = TempData["UserName"].ToString();
                updateCategoryDto.CreateUser = TempData["UserName"].ToString();

                if (updateCategoryDto.File != null && updateCategoryDto.File.Length > 0)
                {
                    // Eski resmi silme işlemi
                    ImageHelper.DeleteImage(updateCategoryDto.ImagePath);

                    // Yeni resmi kaydetme işlemi
                    updateCategoryDto.ImagePath = ImageHelper.SaveImage(updateCategoryDto.File);
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(updateCategoryDto);
            }
        }

        public async Task<IActionResult> IsActiveApproved(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Category/IsActiveAproved?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> IsActiveApprovedCancel(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Category/IsActiveAprovedCancel?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
