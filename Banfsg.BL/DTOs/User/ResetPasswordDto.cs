using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL.DTOs.User
{
    public class ResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
       // [Compare("NewPassword",ErrorMessage ="The New Password and Confirmation New Password don't match!!")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
