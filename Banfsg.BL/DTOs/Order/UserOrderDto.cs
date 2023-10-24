using Banfsg.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class UserOrderDto
{
    public int Id { get; set; }

    public OrderStatus OrderStatus { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? DeliverdDate { get; set; } = null;
    public IEnumerable<ProductOrderedChildDto> Products { get; set; } = new List<ProductOrderedChildDto>();
}
