using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class CategoryRepo : GenericRepo<Category>, ICategoryRepo
{
    private readonly BanfsgContext _context;

    public CategoryRepo(BanfsgContext context) : base(context)
    {
        _context = context;
    }

    public IEnumerable<Product>? GetByIdWithProducts(int id)
    {
        return _context.Set<Product>()
            .Include(p => p.Category)
            .Include(p=>p.Reviews)
            .Where(p => p.CategoryID == id);
            

    
    }
}
