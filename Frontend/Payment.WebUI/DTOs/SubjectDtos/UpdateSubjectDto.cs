using Payment.EntityLayer.Concrete;

namespace Payment.WebUI.DTOs.SubjectDtos
{
    public class UpdateSubjectDto
    {
        public int SubjectID { get; set; }
        public string? Description { get; set; }
        
    }
}
