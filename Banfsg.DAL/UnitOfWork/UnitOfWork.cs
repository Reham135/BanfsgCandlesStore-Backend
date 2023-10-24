using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork

    {
        public IUserRepo UserRepo { get; }
        public IUserAddressesRepo UserAddressesRepo { get; }

        public IUserProductCartRepo UserProductCartRepo { get; }

        public IOrderRepo OrderRepo { get; }

        public IOrderDetailsRepo OrderDetailsRepo { get; }

        public IUserProfileRepo ProfileRepo { get; }

        public IProductRepo ProductRepo { get; }

        public ICategoryRepo CategoryRepo { get; }

        private readonly BanfsgContext _context;
       

        public UnitOfWork(
              BanfsgContext context
            , IUserRepo userRepo
            , IUserAddressesRepo userAddressesRepo
            , IUserProductCartRepo userProductCartRepo
            , IOrderRepo orderRepo
            , IOrderDetailsRepo orderDetailsRepo
            , IUserProfileRepo profileRepo
            , IProductRepo productRepo
            ,ICategoryRepo categoryRepo
            
            )
        {
            _context = context;
            UserRepo = userRepo;
            UserAddressesRepo = userAddressesRepo;
            UserProductCartRepo = userProductCartRepo;
            OrderRepo = orderRepo;
            OrderDetailsRepo = orderDetailsRepo;
            ProfileRepo = profileRepo;
            ProductRepo = productRepo;
            CategoryRepo = categoryRepo;
        }


        public int SaveChanges()
        {
          return _context.SaveChanges();
        }
    }
}
