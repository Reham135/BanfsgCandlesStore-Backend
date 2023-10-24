using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public interface ICategoryRepo : IGenericRepo<Category>
{
    IEnumerable<Product>? GetByIdWithProducts(int id);
}
