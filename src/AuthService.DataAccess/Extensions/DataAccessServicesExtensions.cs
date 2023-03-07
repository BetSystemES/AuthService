using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Entities;
using AuthService.DataAccess.Providers;
using AuthService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.DataAccess.Extensions
{
    /// <summary>
    /// Data layer extenions for <seealso cref="IServiceCollection"/>
    /// </summary>
    public static class DataAccessServicesExtensions
    {
        /// <summary>
        /// Adds the providers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenProvider, RefreshTokenProvider>()
                .AddScoped<IRoleProvider, RoleProvider>()
                .AddScoped<IUserProvider, UserProvider>()
                .AddScoped<IUserRolesProvider, UserRolesProvider>();

            return services;
        }

        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        /// <summary>
        /// Adds the postgres context.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="options">The options.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddPostgresContext(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<AuthDbContext>(options);

            services.AddScoped<IDataContext, AuthDataContext>();
            services.AddTransient<DbContext>(serviceProvider => serviceProvider.GetRequiredService<AuthDbContext>())
                    .AddScopedDbSet<UserRefreshToken>()
                    .AddScopedDbSet<UserRole>()
                    .AddScopedDbSet<User>()
                    .AddScopedDbSet<Role>();

            return services;
        }

        /// <summary>
        /// Adds the scoped database set.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        private static IServiceCollection AddScopedDbSet<TEntity>(this IServiceCollection services)
            where TEntity : class
        {
            services.AddScoped(serviceProvider =>
                serviceProvider.GetRequiredService<AuthDbContext>().Set<TEntity>());

            return services;
        }
    }
}
