using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class ProductRepo : GenericRepo<Product>, IProductRepo
{
    private readonly BanfsgContext _context;

    public ProductRepo(BanfsgContext context) : base(context)
    {
        _context = context;
    }
}
