using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public interface IUnitOfWork
{
    public IUserRepo UserRepo { get; }
    public IUserAddressesRepo UserAddressesRepo { get; }
    public IUserProductCartRepo UserProductCartRepo { get; }
    public IOrderRepo OrderRepo { get; }
    public IOrderDetailsRepo OrderDetailsRepo { get; }
    public IUserProfileRepo ProfileRepo { get; }
    public IProductRepo ProductRepo { get; }
    public ICategoryRepo CategoryRepo { get; }



    int SaveChanges();
}
