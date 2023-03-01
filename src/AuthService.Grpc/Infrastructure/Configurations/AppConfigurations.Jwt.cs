﻿using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Generators;
using AuthService.BusinessLogic.Workers;

namespace AuthService.Grpc.Infrastructure.Configurations
{
    public static partial class AppConfigurations
    {
        /// <summary>
        /// Adds the JWT services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddJwtServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            // TODO: typo in JWTWorker
            services.AddScoped<IJwtWorker, JWTWorker>();

            return services;
        }
    }
}
