using Payment.EntityLayer.Concrete;

namespace Payment.WebUI.DTOs.SubjectDtos
{
    public class ResultSubjectDto
    {
        public int SubjectID { get; set; }
        public string? Description { get; set; }
        public List<Contact> Contacts { get; set; }
    }
}
