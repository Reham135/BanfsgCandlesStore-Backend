using Azure;
using Banfsg.BL;
using Banfsg.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using Banfsg.DAL.UnitOfWork;
using Banfsg.BL.DTOs.User;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Banfsg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _manager;
        private readonly IUsersManager _usersManager;
        private readonly IEmailManager _emailManager;

        public UserController(IUnitOfWork unitOfWork, IConfiguration configuration, Microsoft.AspNetCore.Identity.UserManager<User> manager, IUsersManager usersManager,IEmailManager emailManager)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _manager = manager;
            _usersManager = usersManager;
            _emailManager = emailManager;
        }
        // inject configuration service to get the secretkey from the configuration(appsetting,secrets,environment varialbles)
        //inject UserManager to use the identity functionalities (password hash) after configure it in program.cs



        #region Register

        [HttpPost]
        [Route("Register")]
        public ActionResult<TokenDto> Register(RegisterDto registerDto)
        {
            var newUser = new User
            {
                FName = registerDto.Fname,
                LName = registerDto.Lname,
                Email = registerDto.Email,
                UserName = registerDto.Fname + registerDto.Lname,
                Role = Role.Customer


            };
            var creationResult = _manager.CreateAsync(newUser, registerDto.Password).Result;
            if (!creationResult.Succeeded) { return BadRequest(creationResult.Errors); }
            var claims = new List<Claim>
            {
                new( ClaimTypes.NameIdentifier,newUser.Id),
                new(ClaimTypes.Role,newUser.Role.ToString()),
            };
            var addingClaimsResult = _manager.AddClaimsAsync(newUser, claims).Result;
            return NoContent();

        }
        #endregion


        #region Login


        [HttpPost]
        [Route("Login")]
        public ActionResult<TokenDto> Login(LoginDto credentials)
        {
            User? user = _manager.FindByEmailAsync(credentials.Email).Result;
            if (user == null) { return BadRequest(); }
            bool isPassWordCorrect = _manager.CheckPasswordAsync(user, credentials.Password).Result;
            if (!isPassWordCorrect) { return BadRequest(); }
            //if credentials is true,then generate the token by:
            //1-get user claims
            var claimsList = _manager.GetClaimsAsync(user).Result;
            //2-get the key and convert it to string then  to byte[] then to securitykey object 
            var keyString = _configuration.GetValue<string>("SecretKey");
            var KeyByteArray = Encoding.ASCII.GetBytes(keyString!);   //! because it will not be null never 
            var keySecurityKey = new SymmetricSecurityKey(KeyByteArray);

            //3-Hashing criteria(signing credentials) make a combination from hashing algorithm and secret key in one object
            var signingCredentials = new SigningCredentials(keySecurityKey, SecurityAlgorithms.HmacSha256Signature);
            //4- putting all parts together
            var token = new JwtSecurityToken(
                claims: claimsList,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddMinutes(60)
                );
            //5-create object of tokenHandler to get the final token as string from function writrToken
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return new TokenDto { Token = tokenString };

        }
        #endregion


        #region Forget Password (generate token)
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword([Required] string email)
        {
            User? user = await _manager.FindByEmailAsync(email);
            if (user is null) { return NotFound("User not found with the given email."); }
            var token = await _manager.GeneratePasswordResetTokenAsync(user);
            var forgetPasswordLink = Url.Action(nameof(ResetPassword), "User", new { token, email = user.Email }, Request.Scheme);
            await _emailManager.SendEmailAsync(email, "Reset Password", $"Your reset Password  Link is : {forgetPasswordLink}");

            var response = new
            {
                message = $"Reset Password Email has been Sent Successfully on email:{user.Email}." +
                $"Please open your email & click the link"
            };
            return Ok(response);
        }
        #endregion


        #region Reset Password (Validate token)

        [HttpGet]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPasswordDto { Email = email, Token = token };
            return Ok(model);
        }


        [HttpPost]
        [Route("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordDto userResetPasswordDto)
        {
            User? user = await _manager.FindByEmailAsync(userResetPasswordDto.Email);
            if (user is null) { return NotFound("User not found with the given email."); }
            if (userResetPasswordDto.NewPassword != userResetPasswordDto.ConfirmNewPassword)
            { return BadRequest("The New Password and Confirmation New Password don't match!!"); }
            var result = await _manager.ResetPasswordAsync(user, userResetPasswordDto.Token, userResetPasswordDto.NewPassword);
            if (!result.Succeeded) { return BadRequest(result.Errors); }
            var response = new { message = "Password has been reset successfully :) " };
            return Ok(response);

        }
        #endregion


        


 

      













    }
}

