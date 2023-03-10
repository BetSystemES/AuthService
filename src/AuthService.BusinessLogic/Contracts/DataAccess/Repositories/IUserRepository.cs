using AuthService.BusinessLogic.Entities;

namespace AuthService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Users repository
    /// </summary>
    public interface IUserRepository : IDataRepository<User>
    {
    }
}
