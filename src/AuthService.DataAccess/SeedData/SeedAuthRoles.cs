using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Extensions;
using AuthService.BusinessLogic.Models.Enums;

namespace AuthService.DataAccess.SeedData
{
    /// <summary>
    /// Auth roles for database seeding.
    /// </summary>
    public static class SeedAuthRoles
    {
        /// <summary>
        /// The roles.
        /// </summary>
        public static List<Role> Roles = new()
        {
            new() { Name = AuthRole.Admin.GetDescription() },
            new() { Name = AuthRole.User.GetDescription() },
        };
    }
}
