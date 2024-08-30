using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.DataAccessLayer.Concrete;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.ViewComponents.Courses
{
    public class _CoursesSectionPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _CoursesSectionPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
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
    }
}
