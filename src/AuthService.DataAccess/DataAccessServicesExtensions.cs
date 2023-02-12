using AuthService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.DataAccess
{
    public static class DataAccessServicesExtensions
    {
        /// <summary>
        /// Adds the providers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
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
            return services;
        }
    }
}
