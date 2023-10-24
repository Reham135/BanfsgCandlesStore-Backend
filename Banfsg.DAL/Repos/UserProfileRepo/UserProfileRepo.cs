using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class UserProfileRepo : GenericRepo<User>, IUserProfileRepo
{
    private readonly BanfsgContext _context;

    public UserProfileRepo(BanfsgContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Order>? GetOrdersByUID(string uid)
    {
         return _context.Set<Order>()
               .Where(o => o.UserId == uid)
               .Include(o => o.OrdersProductDetails)
               .ThenInclude(od => od.Product);
    }
}
