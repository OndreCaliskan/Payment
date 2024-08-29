namespace Payment.DtoLayer.Dtos.AddressDtos
{
    public class CreateAddressDto
    {
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public int AppUserId { get; set; }
    }
}
