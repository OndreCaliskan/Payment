namespace Payment.DtoLayer.Dtos.CategoryDtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string ImagePath { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
    }
}
