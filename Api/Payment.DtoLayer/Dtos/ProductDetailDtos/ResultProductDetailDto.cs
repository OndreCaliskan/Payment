namespace Payment.DtoLayer.Dtos.ProductDetailDtos
{
    public class ResultProductDetailDto
    {
        public int ProductDetailID { get; set; }
        public string Description { get; set; }
        public string ProductInfo { get; set; }
        public int ProductID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }

    }
}
