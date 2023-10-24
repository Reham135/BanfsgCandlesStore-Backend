using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class OrderDetailsRepo : GenericRepo<OrderProductDetails>, IOrderDetailsRepo
{
    private readonly BanfsgContext _context;

    public OrderDetailsRepo(BanfsgContext context) : base(context)
    {
        _context = context;
    }

    public void AddRange(IEnumerable<OrderProductDetails> orderProducts)
    {
        _context.Set<OrderProductDetails>().AddRange(orderProducts);
    }
}
