using AuthService.BusinessLogic;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Generators;
using AuthService.BusinessLogic.Providers;
using AuthService.BusinessLogic.Services;
using AuthService.BusinessLogic.Workers;
using AuthService.Grpc.Infrastructure.Mappings;
using BusinessLogic = AuthService.BusinessLogic;

namespace AuthService.Grpc.Infrastructure.Configurations
{
    /// <summary>
    /// App configuration
    /// </summary>
    public static partial class AppConfiguration
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
                .AddScoped<ITokenGenerator, TokenGenerator>()
                .AddScoped<IJwtTokenGenerator, JWTTokenGenerator>()
                .AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>()
                .AddSingleton<IHashProvider, HashProvider>()
                .AddScoped<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
