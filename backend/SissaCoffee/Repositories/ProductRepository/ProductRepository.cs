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
    
    public new async Task<IList<Product>> GetAllAsync()
    {
        return await _table
            .Include(p => p.Ingredients)
            .Include(p => p.Variant)
            .Include(p => p.Tag)
            .ToListAsync();
    }
}