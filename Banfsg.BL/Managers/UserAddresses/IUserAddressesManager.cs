using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.BL;

public interface IUserAddressesManager
{
    IEnumerable<UserAddressDto>? GetAllUserAddressesByUID(string uIDFromToken);
    UserAddressDto? GetAddressById(int id);
     void AddNewAddress(string UIDFromToken, AddingAddressDto address);
     void EditUserAddress(string UID,EditAddressDto address);
    void DeleteByAddressID(int id);
    void SetDefaultAddress(string UID, int id);
}
