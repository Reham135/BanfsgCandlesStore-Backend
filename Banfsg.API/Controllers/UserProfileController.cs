using Banfsg.BL;
using Banfsg.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Drawing;
using System.Security.Claims;

namespace Banfsg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableRateLimiting("fixed")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileManager _profileManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _manager;

        public UserProfileController(IUserProfileManager profileManager, Microsoft.AspNetCore.Identity.UserManager<User> manager)
        {
            _profileManager = profileManager;
            _manager = manager;
        }

        #region Get User Profile
        [HttpGet]
        public ActionResult<UserReadDto> GetCurrentUserProfile() 
        {   //there is 2 ways to get the current user
            //var UID = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;  //this returns the id
            User? user = _manager.GetUserAsync(User).Result;     //the second way by using the identity manager   //this returns the user itself
            if (user == null) { return NotFound("User not logged in "); }
            UserReadDto currentUSer = _profileManager.GetCurrentUSer(user);
            return currentUSer;
        }
        #endregion

        #region Update UserName
        [HttpPut]
        [Route("UserName")]
        public ActionResult UpdateUserName(UserUpdateDto updateDto)
        {
            User? user = _manager.GetUserAsync(User).Result;
            if (user == null) { return NotFound("User not logged in "); }
            _profileManager.UpdateUserName(updateDto,user);
            var response = new { message = "USerNAme has beed updated successfully" };
            return Ok(response);

        }
        #endregion

        #region Delete User
        [HttpDelete]
        public ActionResult deleteUser()
        {
            User? user = _manager.GetUserAsync(User).Result;
            if (user == null) { return NotFound("User not logged in "); }
            _profileManager.DeleteUserByUID(user.Id);
            var response = new { message = "User has beed deleted successfully" };
            return Ok(response);
        }
        #endregion

        #region User Orders
        [HttpGet]
        [Route("Orders")]
        public ActionResult<IEnumerable<UserOrderDto>> GetUserOrders()
        {
            User? user = _manager.GetUserAsync(User).Result;
            if (user == null) { return NotFound("User not logged in "); }
           IEnumerable<UserOrderDto>?UserOrders= _profileManager.GetOrdersByUID(user.Id);
            if(UserOrders == null) 
            {
                var response = new { message = "There has been No Orders yet" };
                return Ok(response); 
            }
            return Ok(UserOrders);

        }
        #endregion

        #region Change Password
        [HttpPut]
        [Route("Change_Password")]
        public async Task<ActionResult> ChangePassword(UserChangePasswordDto changePasswordDto)
        {
            //get the user
            User? user = _manager.GetUserAsync(User).Result; 
            if (user == null) { return NotFound("User not logged in "); }
            //confirm old password
            bool isValidPass = await _manager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword);
            if (!isValidPass) { return NotFound("Old password is not true "); }
            if(changePasswordDto.NewPassword !=changePasswordDto.ConfirmNewPassword)
            { return NotFound("The New Password and Confirmation New Password don't match!!"); }
            //change password
            await _manager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            var response = new { message = "Password has beed updated successfully" };
            return Ok(response);

        }
        #endregion
    }
}
