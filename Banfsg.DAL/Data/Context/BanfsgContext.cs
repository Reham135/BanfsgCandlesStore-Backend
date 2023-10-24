using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Banfsg.DAL;

public class BanfsgContext:IdentityDbContext<User> 
{
    public DbSet<User>Users=>Set<User>();
    public DbSet<Product>Products=>Set<Product>();
    public DbSet<Category>Categories=>Set<Category>();
    public DbSet<Order>Orders=>Set<Order>();
    public DbSet<OrderProductDetails>OrderProductDetails=>Set<OrderProductDetails>();
    public DbSet<Image>ProductImages=>Set<Image>();
    public DbSet<UserAddress> UserAddresses=>Set<UserAddress>();
    public DbSet<Review> Reviews=>Set<Review>();

    public BanfsgContext(DbContextOptions<BanfsgContext>options):base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
