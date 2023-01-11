using SissaCoffee.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SissaCoffee.Data;

public class AppDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
    
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ApplicationRole> ApplicationRoles { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductIngredient> ProductIngredients { get; set; }
}
