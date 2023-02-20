using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace AuthService.Grpc.Infrastructure.Configurations
{
    public static partial class AppConfigurations
    {
        /// <summary>
        /// Adds the serilog logger.
        /// </summary>
        /// <param name="appBuilder">The application builder.</param>
        /// <returns>WebApplicationBuilder</returns>
        public static WebApplicationBuilder AddSerilogLogger(this WebApplicationBuilder appBuilder)
        {
            appBuilder.Host.UseSerilog((_, serviceProvider, config) =>
            {
                config = appBuilder.Environment.IsDevelopment()
                    ? config.MinimumLevel.Verbose()
                    : config.MinimumLevel.Warning();
            });

            return appBuilder;
        }
    }
}
