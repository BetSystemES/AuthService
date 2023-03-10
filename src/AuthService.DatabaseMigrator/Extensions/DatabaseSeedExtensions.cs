using AuthService.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthService.DatabaseMigrator.Extensions
{
    public static class DatabaseSeedExtensions
    {
        private static readonly List<Role> _roles = new()
        {
            new() { Name = "admin" },
            new() { Name = "moderator" },
        };

        public static IServiceProvider AddRolesIfNotPresented<TContext>(this IServiceProvider serviceProvider) where TContext : DbContext
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                var context = serviceProvider.GetRequiredService<TContext>();

                var rolesSet = context.Set<Role>();

                if (rolesSet.FirstOrDefault() is null)
                {
                    rolesSet.AddRange(_roles);

                    context.SaveChanges();
                }

                logger.LogTrace("Roles seed successfully completed");
            }
            catch (Exception e)
            {
                logger.LogCritical("During roles seed error occurred: {Message}", e.Message);

                throw;
            }

            return serviceProvider;
        }
    }
}
