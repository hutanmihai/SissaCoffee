using SissaCoffee.Repositories.RoleRepository;
using SissaCoffee.Repositories.UserRepository;
using SissaCoffee.Services;

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

            return services;
        }
    }
