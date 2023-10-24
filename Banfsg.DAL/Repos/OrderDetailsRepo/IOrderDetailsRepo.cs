using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public interface IOrderDetailsRepo : IGenericRepo<OrderProductDetails>
{
    void AddRange(IEnumerable<OrderProductDetails> orderProducts);
}
