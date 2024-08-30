namespace Payment.WebUI.DTOs.ProductDetailDtos
{
    public class UpdateProductDetailDto
    {
        public int ProductDetailID { get; set; }
        public string Description { get; set; }
        public string ProductInfo { get; set; }
        public int ProductID { get; set; }
        public bool IsActive { get; set; }
    }
}
