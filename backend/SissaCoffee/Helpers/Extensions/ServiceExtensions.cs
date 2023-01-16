using SissaCoffee.Helpers.JwtUtils;
using SissaCoffee.Helpers.Seeders;
using SissaCoffee.Repositories.IngredientRepository;
using SissaCoffee.Repositories.ProductIngredientRepository;
using SissaCoffee.Repositories.ProductRepository;
using SissaCoffee.Repositories.ProductVariantRepository;
using SissaCoffee.Repositories.RoleRepository;
using SissaCoffee.Repositories.TagRepository;
using SissaCoffee.Repositories.UserRepository;
using SissaCoffee.Services.ProductService;
using SissaCoffee.Services.UserService;
using SissaCoffee.Services.RoleService;

namespace SissaCoffee.Helpers.Extensions;

public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IIngredientRepository, IngredientRepository>();
            services.AddTransient<IProductVariantRepository, ProductVariantRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IProductIngredientRepository, ProductIngredientRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IProductService, ProductService>();

            return services;
        }
        
        public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddTransient<RoleSeeder>();
            services.AddTransient<UserSeeder>();
            services.AddTransient<ProductVariantSeeder>();
            services.AddTransient<TagSeeder>();
            services.AddTransient<IngredientSeeder>();
            return services;
        }
        
        public static IServiceCollection AddUtils(this IServiceCollection services)
        {
            services.AddTransient<IJwtUtils, JwtUtils.JwtUtils>();

            return services;
        }
    }
