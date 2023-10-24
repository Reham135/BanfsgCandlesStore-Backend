using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public interface ICategoryManager
{
    IEnumerable<CategoryDto> GetAllCategoriesDto();
    public CategoryDto? GetCategoryById(int id);
    IEnumerable<ProductChildDto>? GetCategoryWithProducts(int id);

}
