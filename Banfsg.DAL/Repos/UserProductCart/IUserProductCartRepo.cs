using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public interface IUserProductCartRepo:IGenericRepo<UserProductCart>
{
    void DeleteAllProductsFromCart(string uID);
    IEnumerable<UserProductCart>? GetAllProductsInCartByUID(string uID);
    int GetCartProductsCounter(string uID);
    UserProductCart? GetProductByCompositeId(string uID, int productId);
}
