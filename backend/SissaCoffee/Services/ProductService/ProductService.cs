using AutoMapper;
using SissaCoffee.Models.DTOs;
using SissaCoffee.Repositories.IngredientRepository;
using SissaCoffee.Repositories.ProductRepository;

namespace SissaCoffee.Services.ProductService;

public class ProductService: IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IIngredientRepository _ingredientRepository;

    public ProductService(IProductRepository productRepository, IIngredientRepository ingredientRepository)
    {
        _productRepository = productRepository;
        _ingredientRepository = ingredientRepository;
    }

    public async Task<List<ProductDTO>> GetAllProductAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var productsDTO = new List<ProductDTO>();
        foreach (var product in products)
        {
            var tag = product.Tag?.Name;
            var ingredientsIds = product.Ingredients?.Select(x => x.IngredientId).ToList();
            var ingredients = new List<string>();
            
            foreach(var ingredientId in ingredientsIds)
            {
                var ingredient = await _ingredientRepository.FindByIdAsync(ingredientId);
                ingredients.Add(ingredient.Name);
            }
            

            productsDTO.Add(new ProductDTO()
            {
                Name = product.Name,
                Size = product.Variant.Size,
                Unit = product.Variant.Unit,
                TagName = tag,
                Ingredients = ingredients
            });
        }

        return productsDTO;
    }
}