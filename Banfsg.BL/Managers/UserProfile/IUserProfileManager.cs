using Banfsg.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public interface IUserProfileManager
{
    void DeleteUserByUID(string id);
    UserReadDto GetCurrentUSer(User user);
    IEnumerable<UserOrderDto>? GetOrdersByUID(string uid);
    void UpdateUserName(UserUpdateDto updateDto,User user);
}
