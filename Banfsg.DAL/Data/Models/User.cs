using Microsoft.AspNetCore.Identity;


namespace Banfsg.DAL;

public class User:IdentityUser
{
    public string FName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public Role Role { get; set; }
    public IEnumerable<UserProductCart> UsersProductsCarts { get; set; } = new HashSet<UserProductCart>();
    public IEnumerable<Order> Orders { get; set; } = new HashSet<Order>();
    public IEnumerable<UserAddress> UserAddresses { get; set; } = new HashSet<UserAddress>();
    public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();
}
