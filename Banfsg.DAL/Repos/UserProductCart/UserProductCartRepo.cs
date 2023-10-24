using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Banfsg.DAL;

public class UserProductCartRepo : GenericRepo<UserProductCart>, IUserProductCartRepo
{
    private readonly BanfsgContext _context;

    public UserProductCartRepo(BanfsgContext context) : base(context)
    {
        _context = context;
    }

    public void DeleteAllProductsFromCart(string uID)
    {
        _context.Set<UserProductCart>().Where(u => u.UserId == uID).ExecuteDelete();
    }

    public IEnumerable<UserProductCart>? GetAllProductsInCartByUID(string uID)
    {
       return  _context.Set<UserProductCart>()
               .Include(c=>c.Product)
               .Where(c=>c.UserId == uID);
    }

    public int GetCartProductsCounter(string uID)
    {
        return _context.Set<UserProductCart>()
                .Where(c => c.UserId == uID)
                .Sum(X => X.Quantity);
    }

    public UserProductCart? GetProductByCompositeId(string uID, int productId)
    {
        return _context.Set<UserProductCart>()
              .Where(c => c.UserId == uID && c.ProductId == productId)
              .FirstOrDefault();
    }
}
