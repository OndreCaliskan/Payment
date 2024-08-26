namespace Payment.WebUI.DTOs.AppUserDtos
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
