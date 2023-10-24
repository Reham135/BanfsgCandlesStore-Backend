using Banfsg.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Banfsg.BL;

namespace Banfsg.BL;

public class UsersManager : IUsersManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly MailSetting _mailSetting;

    public UsersManager(IUnitOfWork unitOfWork, IOptionsMonitor<MailSetting> mailSetting)
    {
        _unitOfWork = unitOfWork;
        _mailSetting = mailSetting.CurrentValue;

    }

    //#region sendingEmail
    //public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    //{
    //    string fromMail = _mailSetting.Email;
    //    string fromPassword = _mailSetting.Password;

    //    MailMessage message = new MailMessage();
    //    message.From = new MailAddress(fromMail);
    //    message.Subject = subject;
    //    message.Body = $"<html><body>{htmlMessage}<br>Thanks,<br> Banfsg Candles </body></html>";
    //    message.IsBodyHtml = true;
    //    message.To.Add(email);

    //    var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
    //    {
    //        Port = _mailSetting.Port,
    //        Credentials = new NetworkCredential(fromMail, fromPassword),
    //        EnableSsl = _mailSetting.EnableSsl,
    //    };

    //    smtpClient.Send(message);

    //}
    //#endregion

}
