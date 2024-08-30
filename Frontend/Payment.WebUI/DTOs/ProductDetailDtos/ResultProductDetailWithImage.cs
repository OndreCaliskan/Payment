namespace Payment.WebUI.DTOs.ProductDetailDtos
{
    public class ResultProductDetailWithImage
    {
        public int ProductDetailID { get; set; }
        public string Description { get; set; }
        public string ProductInfo { get; set; }
        public int ProductID { get; set; }
        public string? CoverImage { get; set; }
    }
}
