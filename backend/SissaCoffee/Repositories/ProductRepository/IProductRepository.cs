using SissaCoffee.Models;
using SissaCoffee.Repositories.GenericRepository;

namespace SissaCoffee.Repositories.ProductRepository;

public interface IProductRepository: IGenericRepository<Product>
{
    public Task<List<Product>> GetAllProductsAsync();

    public Task<Product?> GetProductByIdAsync(Guid id);
}