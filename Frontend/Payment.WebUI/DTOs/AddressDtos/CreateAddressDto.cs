namespace Payment.WebUI.DTOs.AddressDto
{
    public class CreateAddressDto
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public int AppUserId { get; set; }
    }
}
