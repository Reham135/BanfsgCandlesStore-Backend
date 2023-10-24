using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public record RegisterDto
{
    public string Fname { get; init; } = string.Empty;      //init instead of set means when create it cannot be changed
    public string Lname { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
