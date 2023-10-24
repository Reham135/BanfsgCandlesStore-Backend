using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;




namespace Banfsg.BL;

public class EmailManager:IEmailManager
{
    private readonly MailSetting _mailSetting;

    public EmailManager(IOptionsMonitor<MailSetting> mailSetting)
    {
        _mailSetting = mailSetting.CurrentValue;
    }


    #region sending Email
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        string fromMail = _mailSetting.Email;
        string fromPassword = _mailSetting.Password;

        MailMessage message = new MailMessage();
        message.From = new MailAddress(fromMail);
        message.Subject = subject;
        message.Body = $"<html><body>{htmlMessage}<br>Thanks,<br> Banfsg Candles </body></html>";
        message.IsBodyHtml = true;
        message.To.Add(email);

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = _mailSetting.Port,
            Credentials = new NetworkCredential(fromMail, fromPassword),
            EnableSsl = _mailSetting.EnableSsl,
        };

        smtpClient.Send(message);

    }
    #endregion
}
