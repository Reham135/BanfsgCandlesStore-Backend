using Banfsg.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class ProductCartReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public decimal PriceAfter => Math.Round(Price - (Price * Discount / 100), 0);
    public string Color { get; set; } = string.Empty;
    public string Scent { get; set; } = string.Empty;
    public int Quantity { get; set; }

    //public string Image { get; set; } = string.Empty;

}
