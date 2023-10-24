using Banfsg.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class ProductOrderedChildDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal ProductPriceWhenOrdered { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Scent { get; set; } = string.Empty;
    public int Quantity { get; set; }



   
}
