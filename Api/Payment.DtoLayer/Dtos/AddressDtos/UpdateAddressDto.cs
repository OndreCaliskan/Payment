namespace Payment.DtoLayer.Dtos.AddressDtos
{
    public class UpdateAddressDto
    {
        public int AddressID { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
