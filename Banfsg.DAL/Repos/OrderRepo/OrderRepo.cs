using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class OrderRepo : GenericRepo<Order>, IOrderRepo
{
    private readonly BanfsgContext _context;

    public OrderRepo(BanfsgContext context) : base(context)
    {
        _context = context;
    }

    public int GetIdOfLastOrderByUID(string uID)
    {
        return _context.Set<Order>()
            .Where(O => O.UserId == uID)
            .OrderByDescending(O=> O.OrderDate)
            .First().Id;
    }
}
