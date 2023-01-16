using SissaCoffee.Models.DTOs.Product;

namespace SissaCoffee.Services.ProductService;

public interface IProductService
{
    public Task<List<ProductDTO>> GetAllProductsAsync();
}