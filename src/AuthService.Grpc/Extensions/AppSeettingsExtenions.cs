namespace AuthService.Grpc.Extensions
{
    /// <summary>
    /// App settings extensions.
    /// </summary>
    public static class AppSettingsExtenions
    {
        /// <summary>
        /// Configures the application settings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="sectionName">Name of the section.</param>
        public static void ConfigureAppSettings<T>(this IServiceCollection services,
            IConfiguration configuration, string? sectionName = null) where T : class
        {
            services.Configure<T>(configuration.GetSection(string.IsNullOrEmpty(sectionName) ? typeof(T).Name : sectionName));
        }
    }
}
