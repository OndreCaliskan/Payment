

namespace Payment.WebUI.DTOs.ContactDtos
{
    public class CreateContactDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public int SubjectID { get; set; }
        public string Message { get; set; }
    }
}
