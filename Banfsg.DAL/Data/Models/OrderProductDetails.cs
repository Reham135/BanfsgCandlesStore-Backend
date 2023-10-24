using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;
[PrimaryKey("ProductId", "OrderId")] //another way to declare the composite primary key
public class OrderProductDetails
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal ProductPriceWhenOrdered { get; set; }
    public bool IsReviewed { get; set; } = false;
    public Product Product { get; set; } = null!;
    public Order Order { get; set; } = null!;
}
//this table because of many to many relation between order and product