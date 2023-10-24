using Banfsg.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class OrdersManager:IOrdersManager
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdersManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    #region Make New Order
    public void AddNewOrder(string uID, int addressId)
    {
        //1-add new order in database 
        var NewOrder = new Order
        {
            OrderDate = DateTime.Now,
            OrderStatus = OrderStatus.Pending,
            UserId = uID,
            UserAddressId = addressId,

        };
        _unitOfWork.OrderRepo.Add(NewOrder);
        _unitOfWork.SaveChanges();

        //2-get orderId of the new order to use it in orderdetails table
        int lastOrderId = _unitOfWork.OrderRepo.GetIdOfLastOrderByUID(uID);

        //3-transfer products from cart to order Details

        var cartProducts = _unitOfWork.UserProductCartRepo.GetAllProductsInCartByUID(uID);
        if (cartProducts == null) { return; }
        var orderProducts = cartProducts.Select(p => new OrderProductDetails
        {
            OrderId = lastOrderId,
            ProductId = p.ProductId,
            Quantity = p.Quantity,
            ProductPriceWhenOrdered = p.Product.Price * (1 - (p.Product.Discount / 100))
        });
        _unitOfWork.OrderDetailsRepo.AddRange(orderProducts);

        //4-make the cart empty
        _unitOfWork.UserProductCartRepo.DeleteAllProductsFromCart(uID);
        _unitOfWork.SaveChanges();

    }
    #endregion

}
