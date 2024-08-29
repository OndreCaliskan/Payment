﻿using Microsoft.AspNetCore.Mvc;
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
using Payment.WebUI.Helpers;
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
                var responseMessage = await client.GetAsync("https://localhost:7066/api/Product");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                    return View(values);
                }
                return View();
            }
            return RedirectToAction("Index", "Login");

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
            try
            {
                if (model.File != null)
                {
                    model.CoverImage = ImageHelper.SaveImage(model.File);
                }

                model.CreateTime = DateTime.Now;
                model.UpdateTime = DateTime.Now;
                model.CreateUser = TempData["UserName"].ToString();

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
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
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
            try
            {
                if (model.File != null)
                {
                    // Eski resmi silme işlemi
                    ImageHelper.DeleteImage(model.CoverImage);

                    // Yeni resmi kaydetme işlemi
                    model.CoverImage = ImageHelper.SaveImage(model.File);
                }

                model.UpdateTime = DateTime.Now;
                model.UpdateUser = TempData["UserName"].ToString();
                model.CreateUser = TempData["UserName"].ToString();

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync("https://localhost:7066/api/Product/", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
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
