using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class UserRepo : GenericRepo<User>, IUserRepo
{
    private readonly BanfsgContext _context;

    public UserRepo(BanfsgContext context):base(context)
    {
        _context = context;
    }
    public User? GetByEmail(string email)
    {
     return _context.Set<User>().FirstOrDefault(u=>u.Email==email);
    }

    public User? GetUserBYId(string UID)
    {
        return _context.Set<User>().FirstOrDefault(u => u.Id == UID);
    }
}
