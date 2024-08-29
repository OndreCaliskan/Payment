namespace Payment.WebUI.DTOs.ProductDtos
{
    public class CreateProductDto
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountRate { get; set; }
        public int Stock { get; set; }

        public string? CoverImage { get; set; }
        public IFormFile? File { get; set; }

        public decimal? Rating { get; set; }

        public bool? IsActive { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public string CreateUser { get; set; }
    }
}
