using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public interface IUserProductCartManager
{
    IEnumerable<ProductCartReadDto> GetAllProductsInCartByUID(string UID);
    string AddProductToCart(string uID, AddProductCartDto addProductDto);
    string updateProductQuantityInCart(string uID, ProductQuantityCartDto productQuantityCartDto);
    void DeleteProductFromCart(string uID, int id);
    int GetUserCartProductsCounter(string uID);
}
