using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthService.DatabaseMigrator.Extensions
{
    public static class MigrationDatabaseExtensions
    {
        public static IServiceProvider MigrateDatabaseFromContext<TContext>(this IServiceProvider serviceProvider) where TContext : DbContext
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = serviceProvider.GetRequiredService<TContext>();

                context.Database.Migrate();

                logger.LogTrace("Migration successfully completed");
            }
            catch (Exception e)
            {
                logger.LogCritical("During migration error occurred: {Message}", e.Message);

                throw;
            }

            return serviceProvider;
        }
    }
}
