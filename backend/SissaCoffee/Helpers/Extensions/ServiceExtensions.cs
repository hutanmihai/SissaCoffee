using SissaCoffee.Helpers.Seeders;
using SissaCoffee.Repositories.RoleRepository;
using SissaCoffee.Repositories.UserRepository;
using SissaCoffee.Services.UserService;
using SissaCoffee.Services.RoleService;

namespace SissaCoffee.Helpers.Extensions;

public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();

            return services;
        }
        
        public static IServiceCollection AddSeeders(this IServiceCollection services)
        {
            services.AddTransient<RoleSeeder>();
            services.AddTransient<UserSeeder>();
            return services;
        }
    }
