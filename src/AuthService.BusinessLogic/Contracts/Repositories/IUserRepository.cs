using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Repositories
{
    /// <summary>
    /// Users repository
    /// </summary>
    public interface IUserRepository : IDataRepository<User>
    {
    }
}
