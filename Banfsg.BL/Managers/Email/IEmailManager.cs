using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public interface IEmailManager
{
    Task SendEmailAsync(string mailTo, string subject, string body);

}
