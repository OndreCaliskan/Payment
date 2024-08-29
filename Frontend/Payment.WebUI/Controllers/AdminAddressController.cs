using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.AddressDto;
using System.Text;

namespace Payment.WebUI.Controllers
{
    public class AdminAddressController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminAddressController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.GetAsync("https://localhost:7066/api/Address/AddressListWithUserName");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();
                var Addresss = JsonConvert.DeserializeObject<List<AddressWithUsernameDto>>(result);
                return View(Addresss);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressDto createAppAddressDto)
        {
            var client = _httpClientFactory.CreateClient("");
            var content = new StringContent(JsonConvert.SerializeObject(createAppAddressDto), Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7066/api/Address", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(createAppAddressDto);
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
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAppAddressDto)
        {
            var client = _httpClientFactory.CreateClient("");
            var jsonData = JsonConvert.SerializeObject(updateAppAddressDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/Address", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> DeleteAddress(int id)
        {

            var client = _httpClientFactory.CreateClient("");
            var responseMessage = await client.DeleteAsync($"https://localhost:7066/api/Address/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

