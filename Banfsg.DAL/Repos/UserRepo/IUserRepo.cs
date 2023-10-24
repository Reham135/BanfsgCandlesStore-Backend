using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public interface IUserRepo:IGenericRepo<User>
{
    User? GetByEmail(string email);
    User? GetUserBYId(string UID);
}
