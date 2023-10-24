using Banfsg.BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace Banfsg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("fixed")]
    public class UserAddressesController : ControllerBase
    {
        private readonly IUserAddressesManager _userAddressesManager;

        public UserAddressesController(IUserAddressesManager userAddressesManager)
        {
            _userAddressesManager = userAddressesManager;
        }


        #region Get All User Addresses

        [HttpGet]
        public ActionResult<IEnumerable<UserAddressDto>> GetAllUserAdresses()
        {
            var UIDFromToken = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UIDFromToken == null) { return BadRequest("Not Logged in !!"); }
            var addresses = _userAddressesManager.GetAllUserAddressesByUID(UIDFromToken);
            return Ok(addresses);
        }

        #endregion

        #region Get User Address  by Id
        [HttpGet]
        [Route("{id}")]
        public ActionResult<UserAddressDto> GetAddressById(int id)
        {
            var UID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UID == null) { return BadRequest("Not Logged in !!"); }
            var address = _userAddressesManager.GetAddressById(id);
            return Ok(address);
        }

        #endregion

        #region Add New Address
        [HttpPost]
        public ActionResult addNewAddress(AddingAddressDto addingAddressDto)
        {
            var UID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UID == null) { return BadRequest("Not Logged in !!"); }
            _userAddressesManager.AddNewAddress(UID, addingAddressDto);
            var response = new { message = "Address has been Added successfully" };
            return Ok(response);
        }

        #endregion

        #region Edit Address
        [HttpPut] 
        public ActionResult editUserAddress(EditAddressDto editAddressDto)
        {
            var UID=User.Claims.FirstOrDefault(c=>c.Type==ClaimTypes.NameIdentifier)?.Value;
            if (UID == null) { return BadRequest("Not Logged in !!"); }
            _userAddressesManager.EditUserAddress(UID,editAddressDto);
            var response = new { message = "Address has been edited successfully" };
            return Ok(response );
        }

        #endregion

        #region Delete Address
        [HttpDelete]
        [Route("{id}")]
        public ActionResult deleteAddress(int id) 
        {
            var UID = User.Claims.FirstOrDefault(c=>c.Type== ClaimTypes.NameIdentifier)?.Value;
            if (UID == null) { return BadRequest("Not Logged in !!"); }
            _userAddressesManager.DeleteByAddressID(id);
            var response = new { message = "Address has been deleted successfully" };
            return Ok();
        }
        #endregion

        #region Set Default Address 
        [HttpPatch]
        [Route("setDefaultAddress/{id}")]
        public ActionResult SetDefaultAddress(int id) 
        {
            var UID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (UID == null) { return BadRequest("Not Logged in !!"); }
            _userAddressesManager.SetDefaultAddress(UID,id);
            return Ok();
        }
        #endregion


    }
}
