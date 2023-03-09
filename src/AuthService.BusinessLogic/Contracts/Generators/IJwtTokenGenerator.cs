using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Contracts.Generators
{
    /// <summary>
    /// Jwt token generator contract.
    /// </summary>
    public interface IJwtTokenGenerator
    {
        /// <summary>
        /// Generates the specified token.
        /// </summary>
        /// <typeparam name="TToken">The type of the token.</typeparam>
        /// <param name="user">The user.</param>
        /// <param name="issuedAtUtc">The issued at UTC.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Generic TToken.</returns>
        TToken Generate<TToken>(User user, DateTime issuedAtUtc, CancellationToken cancellationToken) where TToken : JwtToken, new();
    }
}
