using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class UserAddressesRepo : GenericRepo<UserAddress>, IUserAddressesRepo
{
    private readonly BanfsgContext _context;

    public UserAddressesRepo(BanfsgContext context) : base(context)
    {
        _context = context;
    }

    #region Get All User Addresses
    public IEnumerable<UserAddress> GetAllUserAddressesByUID(string uidFromToken)
    {
        return _context.Set<UserAddress>().Where(u => u.UserId == uidFromToken);
    }
    #endregion

    #region Reset Default Address
    public void resetDefaultAddress(string uid)
    {
        _context.Set<UserAddress>()
       .Where(u => u.UserId == uid)
       .ExecuteUpdate(setters => setters.SetProperty(u => u.DefaultAddress, false));

    }
    #endregion

}
