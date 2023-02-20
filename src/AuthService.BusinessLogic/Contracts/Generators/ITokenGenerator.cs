using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Generators
{
    /// <summary>
    /// Token processor
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates the token asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Token <seealso cref="Token"/></returns>
        Task<Token> GenerateTokenAsync(User user, CancellationToken cancellationToken);
    }
}
