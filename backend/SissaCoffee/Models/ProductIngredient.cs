using SissaCoffee.Models.Base;

namespace SissaCoffee.Models;

public class ProductIngredient: BaseEntity
{
    public Product Product { get; set; }
    public Ingredient Ingredient { get; set; }
    
    public Guid ProductId { get; set; }
    public Guid IngredientId { get; set; }
}