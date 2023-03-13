using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Extensions;
using AuthService.BusinessLogic.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthService.DatabaseMigrator.Extensions
{
    public static class DatabaseSeedExtensions
    {
        private static readonly List<Role> _seedRoles = new()
        {
            new() { Name = AuthRole.Admin.GetDescription() },
            new() { Name = AuthRole.User.GetDescription() },
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
                    rolesSet.AddRange(_seedRoles);

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
