using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.ProductDetailDtos;

namespace Payment.WebUI.ViewComponents.ProductDetail
{
    public class _ProductDetailTabsSectionPartial : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public _ProductDetailTabsSectionPartial(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var client = _clientFactory.CreateClient("");
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/ProductDetail/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var productDetail = JsonConvert.DeserializeObject<ResultProductDetailDto>(content);
                return View(productDetail);
            }
            return View();
        }
    }
}
