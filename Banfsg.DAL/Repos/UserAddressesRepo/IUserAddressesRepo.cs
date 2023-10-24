using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public interface IUserAddressesRepo:IGenericRepo<UserAddress>
{ IEnumerable<UserAddress>?GetAllUserAddressesByUID(string uidFromToken);
  void resetDefaultAddress(string uid);
}
