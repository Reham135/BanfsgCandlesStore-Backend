using Microsoft.EntityFrameworkCore;

namespace Banfsg.DAL;
[PrimaryKey("UserId", "ProductId")]

public class UserProductCart
{
    public int ProductId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public Product Product { get; set; } = null!;
    public User User { get; set; } = null!;
}
//this table because of many to many relation between user and product