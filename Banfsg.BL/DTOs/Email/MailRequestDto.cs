using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class MailRequestDto
{
    public string ToEmail { get; set; }=string.Empty;
    public string subject { get; set; }=string.Empty;
    public string body { get; set; }=string.Empty;
}
