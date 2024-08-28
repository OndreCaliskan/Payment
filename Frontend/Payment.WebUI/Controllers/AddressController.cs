using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.AddressDto;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class AddressController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AddressController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> UserAddresses(int id)
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync("https://localhost:7066/api/User/GetUserAddresses");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var userAddresses = JsonConvert.DeserializeObject<List<ResultAddressDto>>(result);
                return View(userAddresses);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateAddress()
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync("https://localhost:7066/api/User/GetUserID");
            if (responseMessage.IsSuccessStatusCode)
            {
                var userID = await responseMessage.Content.ReadFromJsonAsync<int>();
                ViewBag.UserID = userID;
                TempData["UserID"] = userID;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
        {
            var userID = TempData["UserID"];
            createAddressDto.AppUserId = (int)userID;
            var client = _httpClientFactory.CreateClient("");
            var jsonData=JsonConvert.SerializeObject(createAddressDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/Address", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserAddresses");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAddress(int id)
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/Address/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<UpdateAddressDto>(jsonData);
                return View(value);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            var client = _httpClientFactory.CreateClient("");
            var jsonData = JsonConvert.SerializeObject(updateAddressDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/Address", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("UserAddresses");
            }
            return View();
        }
    }
}
