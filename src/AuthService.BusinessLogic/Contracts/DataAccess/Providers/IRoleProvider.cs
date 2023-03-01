using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.DataAccess.Providers
{
    // TODO: change file location to AuthService.DataAccess.Contracts.Providers
    /// <summary>
    /// Role provider
    /// </summary>
    public interface IRoleProvider
    {
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>List of roles</returns>
        Task<List<Role>> GetAll(CancellationToken token);
    }
}
