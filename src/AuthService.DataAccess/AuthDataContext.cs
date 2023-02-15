using AuthService.BusinessLogic.Contracts.DataAccess;

namespace AuthService.DataAccess
{
    /// <summary>
    /// Auth data context.
    /// </summary>
    /// <seealso cref="BusinessLogic.Contracts.DataAccess.IDataContext" />
    public class AuthDataContext : IDataContext
    {
        private readonly AuthDbContext _authDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthDataContext"/> class.
        /// </summary>
        public AuthDataContext(AuthDbContext authDbContext)
        {
            _authDbContext = authDbContext;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Task
        /// </returns>
        public Task SaveChanges(CancellationToken token)
        {
            return _authDbContext.SaveChangesAsync(token);
        }
    }
}
