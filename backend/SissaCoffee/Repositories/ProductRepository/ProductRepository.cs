using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SissaCoffee.Repositories.GenericRepository;
using SissaCoffee.Data;
using SissaCoffee.Models;
using SissaCoffee.Models.DTOs.Product;

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
}