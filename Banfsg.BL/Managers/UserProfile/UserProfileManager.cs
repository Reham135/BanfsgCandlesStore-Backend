using Banfsg.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public class UserProfileManager:IUserProfileManager
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileManager(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    

    #region Get User Profile
    public UserReadDto GetCurrentUSer(User user)
    {
        UserReadDto currentUser = new UserReadDto
        {
            Email = user.Email!,
            FName = user.FName,
            LName = user.LName
        };
        return currentUser;
    }
    #endregion

    #region Update UserName
    public void UpdateUserName(UserUpdateDto updateDto, User user)
    {
        user.FName = updateDto.FName;
        user.LName = updateDto.LName;
        _unitOfWork.SaveChanges();

    }
    #endregion

    #region Delete User
    public void DeleteUserByUID(string id)
    {
      var userFromDb=  _unitOfWork.UserRepo.GetUserBYId(id);
        if (userFromDb == null) { return; }
        _unitOfWork.ProfileRepo.Delete(userFromDb);
        _unitOfWork.SaveChanges();
    }

    #endregion

    #region Get User Orders
    public IEnumerable<UserOrderDto>? GetOrdersByUID(string uid)
    {
      IEnumerable<Order>? ordersFromDb=  _unitOfWork.ProfileRepo.GetOrdersByUID(uid);
        if(ordersFromDb == null) {  }
        IEnumerable<UserOrderDto> OrdersDto = ordersFromDb.Select(order => new UserOrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            DeliverdDate = order.DeliverdDate,
            OrderStatus = order.OrderStatus,
            Products = order.OrdersProductDetails.Select(od => new ProductOrderedChildDto
            {
                Id=od.ProductId,
                Name=od.Product.Name,
                Scent = od.Product.Scent,
                Color = od.Product.Color,
                ProductPriceWhenOrdered = od.ProductPriceWhenOrdered,
                Quantity=od.Quantity

            })
        }) ;
        return OrdersDto;

    }
    #endregion
}
