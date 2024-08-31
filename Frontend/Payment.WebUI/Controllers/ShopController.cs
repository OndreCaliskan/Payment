﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.DataAccessLayer.Concrete;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.Controllers
{
    [AllowAnonymous]
    public class ShopController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ShopController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            using (var context = new Context())
            {
                ViewBag.CategoryName = context.Categories.ToDictionary(c => c.Id, c => c.Name);

            }

            var responseMessage = await client.GetAsync("https://localhost:7066/api/AdminProduct");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();


        }
    }
}
