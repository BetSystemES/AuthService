using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.DataAccess.Repositories
{
    // TODO: change file location to AuthService.DataAccess.Contracts.Repositories
    /// <summary>
    /// Refresh token repository
    /// </summary>
    public interface IRefreshTokenRepository : IDataRepository<UserRefreshToken>
    {
    }
}
