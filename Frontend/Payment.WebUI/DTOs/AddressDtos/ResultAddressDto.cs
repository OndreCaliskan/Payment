﻿namespace Payment.WebUI.DTOs.AddressDto
{
    public class ResultAddressDto
    {
        public int AddressID { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string CreateUser { get; set; }
        public string UpdateUser { get; set; }
        public int AppUserId { get; set; }
    }
}