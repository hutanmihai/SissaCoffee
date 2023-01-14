using SissaCoffee.Models.DTOs;

namespace SissaCoffee.Services.ProductService;

public interface IProductService
{
    public Task<List<ProductDTO>> GetAllProductAsync();
}