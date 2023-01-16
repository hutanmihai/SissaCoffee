using Microsoft.EntityFrameworkCore;
using SissaCoffee.Repositories.GenericRepository;
using SissaCoffee.Data;
using SissaCoffee.Models;

namespace SissaCoffee.Repositories.ProductRepository;

public class ProductRepository: GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _table
            .Include(p => p.Ingredients)
            .ThenInclude(x => x.Ingredient)
            .Include(p => p.Variant)
            .Include(p => p.Tag)
            .ToListAsync();
    }
    
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        var product = await _table
            .Include(p => p.Ingredients)
            .ThenInclude(pi => pi.Ingredient)
            .Include(p => p.Variant)
            .Include(p => p.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);
        return product ?? null;
    }
}