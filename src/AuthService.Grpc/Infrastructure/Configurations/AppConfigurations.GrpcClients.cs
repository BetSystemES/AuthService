using AuthService.Grpc.Infrastructure.Configurations;
using AuthService.Grpc.Infrastructure.ConfigurationSettings;
using AuthService.Grpc.Settings;
using Grpc.Core;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Options;
using static ProfileService.GRPC.ProfileService;

namespace AuthService.Grpc.Infrastructure.Configurations
{
    public static partial class AppConfigurations
    {
        /// <summary>
        /// Adds the GRPC clients.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddGrpcClients(this IServiceCollection services)
        {
            services
                .AddGrpcClient<ProfileServiceClient>();

            return services;
        }

        /// <summary>
        /// Adds the GRPC client.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">serviceEndpoint</exception>
        private static IServiceCollection AddGrpcClient<T>(this IServiceCollection services) where T : class
        {
            var serviceEndpointsSettings = services.BuildServiceProvider().GetService<IOptions<ServiceEndpointsSettings>>()!.Value;

            var serviceName = typeof(T).Name;

            var serviceEndpoint = serviceEndpointsSettings?.ServiceEndpoints
                .FirstOrDefault(x => x.Name.Equals(serviceName));

            ArgumentNullException.ThrowIfNull(serviceEndpoint, nameof(serviceEndpoint));

            return services
                .AddGrcpServiceClient<T>(serviceName, serviceEndpoint.Url);
        }

        /// <summary>
        /// Adds the GRCP service client.
        /// </summary>
        /// <typeparam name="TClient">The type of the client.</typeparam>
        /// <param name="services">The services.</param>
        /// <param name="clientName">Name of the client.</param>
        /// <param name="endpoint">The endpoint.</param>
        /// <returns></returns>
        private static IServiceCollection AddGrcpServiceClient<TClient>(this IServiceCollection services, string clientName, string endpoint) where TClient : class
        {
            return services
                .AddGrpcClient<TClient>(clientName, options =>
                {
                    options.Address = new Uri(endpoint);
                    options.ChannelOptionsActions.Add(options =>
                     {
                         options.ServiceConfig = new ServiceConfig { MethodConfigs = { GrpcRetryPolicyConfiguration.DefaultMethodConfig } };
                     });
                })
                .Services;
        }
    }
}
