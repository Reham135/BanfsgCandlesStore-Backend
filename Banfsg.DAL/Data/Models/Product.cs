namespace Banfsg.DAL;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public string Scent { get; set; } = string.Empty;
    public int CategoryID { get; set; }
    public Category Category { get; set; } = null!;
    public IEnumerable<UserProductCart> UsersProductsCarts { get; set; } = new HashSet<UserProductCart>();
    public IEnumerable<OrderProductDetails> OrdersProductDetails { get; set; } = new HashSet<OrderProductDetails>();
    public IEnumerable<Review> Reviews { get; set; } = new HashSet<Review>();
}
