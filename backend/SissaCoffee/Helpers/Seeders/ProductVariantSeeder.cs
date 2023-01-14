using SissaCoffee.Models;
using SissaCoffee.Repositories.ProductVariantRepository;

namespace SissaCoffee.Helpers.Seeders;

public class ProductVariantSeeder
{
    private readonly IProductVariantRepository _productVariantRepository;
    
    public ProductVariantSeeder(IProductVariantRepository productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }

    public void SeedProductVariants()
    {
        var productVariants = new List<ProductVariant>()
        {
            new()
            {
                Name = "Small",
                Size = 50,
                Unit = "ml",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Name = "Tall",
                Size = 200,
                Unit = "ml",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Name = "Grande",
                Size = 350,
                Unit = "ml",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new()
            {
                Name = "Venti",
                Size = 450,
                Unit = "ml",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
        };

        foreach (var productVariant in productVariants)
        {
            if (_productVariantRepository.FindByName(productVariant.Name) is null)
            {
                _productVariantRepository.Create(productVariant);
            }
        }
    }
}