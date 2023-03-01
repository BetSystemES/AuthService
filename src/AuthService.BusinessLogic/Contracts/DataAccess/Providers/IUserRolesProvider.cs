using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.DataAccess.Providers
{
    // TODO: change file location to AuthService.DataAccess.Contracts.Providers
    /// <summary>
    /// User roles provider
    /// </summary>
    public interface IUserRolesProvider
    {
        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>List of Roles</returns>
        Task<List<Role?>> GetUserRoles(Guid userId, CancellationToken token);
    }
}
