using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Payment.WebUI.DTOs.ProductDetailDtos;
using Payment.WebUI.DTOs.ProductDtos;

namespace Payment.WebUI.ViewComponents.ProductDetail
{
    public class _ProductDetailProductInfoPartial : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public _ProductDetailProductInfoPartial(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var coverImage = "";
            var client = _clientFactory.CreateClient("");
            var responseMessageProduct= await client.GetAsync($"https://localhost:7066/api/Product/GetProductWithCategoryNameById?id={id}");
            if (responseMessageProduct.IsSuccessStatusCode)
            {
                var contentProduct = await responseMessageProduct.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<ResultProductDto>(contentProduct);
                coverImage = product.CoverImage;
            }

            /**/
            var responseMessage = await client.GetAsync($"https://localhost:7066/api/ProductDetail/GetProductDetailByProductId?productId={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var productDetailId = JsonConvert.DeserializeObject<int>(content);

                var responseMessageProductDetail = await client.GetAsync($"https://localhost:7066/api/ProductDetail/{productDetailId}");
                if (responseMessageProductDetail.IsSuccessStatusCode)
                {
                    var contentProduct = await responseMessageProductDetail.Content.ReadAsStringAsync();
                    var productDetail = JsonConvert.DeserializeObject<ResultProductDetailDto>(contentProduct);
                    return View(new ResultProductDetailWithImage
                    {
                        ProductDetailID = productDetail.ProductDetailID,
                        ProductID = productDetail.ProductID,
                        Description = productDetail.Description,
                        ProductInfo=productDetail.ProductInfo,
                        CoverImage =coverImage
                    });
                }
            }
            return View();
        }
    }
}
///   /img/0e2490d2-c8a1-4099-97ce-b41585605e5a.jpg