namespace Payment.WebUI.DTOs.AddressDto
{
    public class UpdateAddressDto
    {
        public int AddressID { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public int AppUserId { get; set; }
    }
}
