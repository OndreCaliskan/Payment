namespace Payment.DtoLayer.Dtos.AddressDtos
{
    public class AddressWithUsernameDto
    {
        public int AddressID { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UserName { get; set; }
    }
}
