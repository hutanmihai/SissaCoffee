using SissaCoffee.Models.Base;

namespace SissaCoffee.Models;

public class Product: BaseEntity
{
    public string Name { get; set; } = String.Empty;
    
    public Tag? Tag { get; set; }
    public Guid? TagId { get; set; }
    
    public ProductVariant Variant { get; set; }
    public Guid VariantId { get; set; }
    
    public ICollection<ProductIngredient> Ingredients { get; set; }
}