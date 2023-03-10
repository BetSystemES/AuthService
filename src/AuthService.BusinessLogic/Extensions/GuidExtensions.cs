using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Extensions
{
    public static class GuidExtensions
    {
        public static IEnumerable<UserRole> ToUserRoles(this IEnumerable<Guid> roleIds, Guid userId)
        {
            return roleIds.Select(x => new UserRole() { RoleId = x, UserId = userId });
        }
    }
}
