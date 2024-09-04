using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment.BusinessLayer.Abstract;
using Payment.DtoLayer.Dtos.AddressDtos;
using Payment.EntityLayer.Concrete;

namespace Payment.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;

        public AddressController(IAddressService addressService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _addressService = addressService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult AddressList()
        {
            var values = _addressService.TGetList();
            return Ok(values);
        }

        [HttpGet("AddressListWithUserName")]
        public IActionResult AddressListWithUserName()
        {
            var values = _addressService.TAddressWithUsername();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetAddress(int id)
        {
            var value = _addressService.TGetByID(id);
            if (value == null)
            {
                return NotFound("Address Not Found.");
            }
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressDto createAddressDto)
        {
            var user = await _userManager.GetUserAsync(User);
            var value = _mapper.Map<Address>(createAddressDto);
            value.CreateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            value.UpdateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            value.CreateUser = user.UserName;
            value.UpdateUser = user.UserName;
            _addressService.TInsert(value);
            return Ok("Address added.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressDto updateAddressDto)
        {
            var address = _addressService.TGetByID(updateAddressDto.AddressID);
            if (address == null)
            {
                return NotFound("Address Not Found.");
            }
            var user = await _userManager.GetUserAsync(User);
            address.AddressID = updateAddressDto.AddressID;
            address.AddressLine = updateAddressDto.AddressLine;
            address.City = updateAddressDto.City;
            address.District = updateAddressDto.District;
            address.CreateTime = address.CreateTime;
            address.UpdateTime = DateTime.Parse(DateTime.Now.ToShortDateString());
            address.CreateUser = address.CreateUser;
            address.UpdateUser = user.UserName;
            address.AppUserId = address.AppUserId;
            _addressService.TUpdate(address);
            return Ok("Address updated.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            var value = _addressService.TGetByID(id);
            if (value == null)
            {
                return NotFound("Address Not Found.");
            }
            _addressService.TDelete(value);
            return Ok("Address deleted.");
        }
    }
}
