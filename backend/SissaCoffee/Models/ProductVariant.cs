using SissaCoffee.Models.Base;

namespace SissaCoffee.Models;

public class ProductVariant: BaseEntity
{
    public int Size { get; set; }
    public string Unit { get; set; } = String.Empty;
    
    public ICollection<Product> Products { get; set; }
}