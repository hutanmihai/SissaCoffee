using SissaCoffee.Models.DTOs.Product;

namespace SissaCoffee.Services.ProductService;

public interface IProductService
{
    public Task<List<ProductDTO>> GetAllProductsAsync();
    public Task UpdateProductAsync(Guid id, ProductUpdateDTO productUpdateDTO);
    public Task<ProductDTO> CreateProductAsync(ProductCreateDTO productCreateDTO);
    public Task DeleteProductAsync(Guid id);
}