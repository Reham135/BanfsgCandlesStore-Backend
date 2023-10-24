using Banfsg.BL;
using Banfsg.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Banfsg.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly UserManager<User> _manager;
        private readonly IEmailManager _emailManager;

        public EmailController(UserManager<User> manager,IEmailManager emailManager)
        {
            _manager = manager;
            _emailManager = emailManager;
        }




        #region SendEmail
        [HttpPost]
        [Route("SendEmail")]
        public async Task<ActionResult> SendEmail(MailRequestDto mailRequestDto)
        {
            await _emailManager.SendEmailAsync(mailRequestDto.ToEmail, mailRequestDto.subject, mailRequestDto.body);
            var response = new { message = "Email has been sent successfully" };
            return Ok(response);
        }
        #endregion
    }
}
