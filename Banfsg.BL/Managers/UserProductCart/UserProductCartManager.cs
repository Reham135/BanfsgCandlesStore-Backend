using Banfsg.DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class UserProductCartManager: IUserProductCartManager
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProductCartManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #region Add Product to cart
    public string AddProductToCart(string UID, AddProductCartDto addProductDto)
    {
        string status= "Product Added";
        UserProductCart? productFromDb = _unitOfWork.UserProductCartRepo.GetProductByCompositeId(UID, addProductDto.ProductId);
        if (productFromDb == null)       //check if the product already in the user cart or not
        {
            if (addProductDto.Quantity > 10)      //if not ,check quentity then add the product
            {
                addProductDto.Quantity = 10;
                status = "Maximum quantity is 10 ";
            }
            var productToAddToCart = new UserProductCart
            {
                ProductId = addProductDto.ProductId,
                Quantity = addProductDto.Quantity,
                UserId = UID
            };
            _unitOfWork.UserProductCartRepo.Add(productToAddToCart);

        }
        else                                  //if already exists,update the quantity only
        {
            productFromDb.Quantity += addProductDto.Quantity;
            if(productFromDb.Quantity > 10)
            {
                productFromDb.Quantity = 10;
                status = "Product Exist in cart so ,Quantity Updated to only 10 pieces Max";
            }
        }
        _unitOfWork.SaveChanges();
        return status;
        
    }
    #endregion

    #region Get All Products In Cart By User Id from token

    public IEnumerable<ProductCartReadDto> GetAllProductsInCartByUID(string UID)
    {
        IEnumerable<UserProductCart>? productsFromDb = _unitOfWork.UserProductCartRepo.GetAllProductsInCartByUID(UID);
        IEnumerable<ProductCartReadDto> products = productsFromDb.Select(p => new ProductCartReadDto
        {
            Id = p.ProductId,
            Name = p.Product.Name,
            Color = p.Product.Color,
            Discount = p.Product.Discount,
            Price = p.Product.Price,
            Quantity = p.Quantity,
            Scent = p.Product.Scent

        });
        return products;
    }
    #endregion

    #region update quantity of product in cart
    public string updateProductQuantityInCart(string uID, ProductQuantityCartDto productQuantityCartDto)
    {
        string status = "product Quantity Edited in userCart";// we will use it in controller to send the message to user
        var productFromDb = _unitOfWork.UserProductCartRepo.GetProductByCompositeId(uID, productQuantityCartDto.ProductId);
        if (productFromDb != null)
        {
            if (productQuantityCartDto.Quantity > 10)
            {
                productQuantityCartDto.Quantity = 10;
                status = "product Quantity Edited in userCart to 10 pieces Max";
            }
            productFromDb.Quantity = productQuantityCartDto.Quantity;
            _unitOfWork.SaveChanges();
        }
        return status;
    }
    #endregion

    #region Delete product from cart
    public void DeleteProductFromCart(string uID, int id)
    {
        UserProductCart? productFromDb = _unitOfWork.UserProductCartRepo.GetProductByCompositeId(uID, id);
        _unitOfWork.UserProductCartRepo.Delete(productFromDb!);
        _unitOfWork.SaveChanges();
    }
    //another way
    //UserProductsCart productToRemove = new UserProductsCart
    //{
    //    ProductId = id,
    //    UserId = UID,
    //};
    //_unitOfWork.UserProdutsCartRepo.Delete(productToRemove);
    //    _unitOfWork.Savechanges();

    #endregion

    #region Get Cart Products Counter
    public int GetUserCartProductsCounter(string uID)
    {
       return _unitOfWork.UserProductCartRepo.GetCartProductsCounter(uID);
    }
    #endregion
}
