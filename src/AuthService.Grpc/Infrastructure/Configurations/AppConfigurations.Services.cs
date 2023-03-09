using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Providers;
using AuthService.BusinessLogic.Services;
using AuthService.Grpc.Infrastructure.Mappings;

namespace AuthService.Grpc.Infrastructure.Configurations
{
    /// <summary>
    /// App configuration
    /// </summary>
    public static partial class AppConfigurations
    {
        /// <summary>Adds the infrastructure services to service collection</summary>
        /// <param name="services">The service collection.</param>
        /// <returns> IServiceCollection </returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthServiceProfile).Assembly);

            return services;
        }

        /// <summary>Adds the infrastructure services to service collection</summary>
        /// <param name="services">The service collection.</param>
        /// <returns> IServiceCollection  </returns>
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, BusinessLogic.Services.AuthService>()
                .AddScoped<IUserService, UserService>()
                .AddSingleton<IHashProvider, HashProvider>()
                .AddScoped<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
