using Payment.EntityLayer.Concrete;

namespace Payment.WebUI.DTOs.ContactDtos
{
    public class ResultContactDto
    {
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public int SubjectID { get; set; }
        public Subject Subject { get; set; }
        public string Message { get; set; }
    }
}
