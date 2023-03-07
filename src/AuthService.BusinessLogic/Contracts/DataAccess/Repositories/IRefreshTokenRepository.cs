using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Refresh token repository
    /// </summary>
    public interface IRefreshTokenRepository : IDataRepository<UserRefreshToken>
    {
    }
}
