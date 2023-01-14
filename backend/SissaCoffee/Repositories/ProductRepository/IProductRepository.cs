using SissaCoffee.Models;
using SissaCoffee.Repositories.GenericRepository;

namespace SissaCoffee.Repositories.ProductRepository;

public interface IProductRepository: IGenericRepository<Product>
{
    public new Task<IList<Product>> GetAllAsync();
}