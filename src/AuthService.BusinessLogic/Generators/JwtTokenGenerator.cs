using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;
using AuthService.BusinessLogic.Models.AppSettings;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic.Generators
{
    /// <summary>
    /// Jwt token generator implementation.
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Generators.IJwtTokenGenerator" />
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtConfig _jwtConfig;
        private readonly IJwtWorker _jwtWorker;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
        /// </summary>
        /// <param name="jwtConfig">The JWT configuration.</param>
        /// <param name="jwtWorker">The JWT worker.</param>
        public JwtTokenGenerator(IOptions<JwtConfig> jwtConfig, IJwtWorker jwtWorker)
        {
            _jwtConfig = jwtConfig.Value;
            _jwtWorker = jwtWorker;
        }

        /// <inheritdoc/>
        public TToken Generate<TToken>(User user, DateTime issuedAtUtc, IEnumerable<Role> roles, CancellationToken cancellationToken) where TToken : JwtToken, new()
        {
            var expiresAtUtc = typeof(TToken).Name switch
            {
                nameof(RefreshToken) => _jwtConfig.RefreshTokenLifetimeInMinutes,
                nameof(AccessToken) => _jwtConfig.TokenLifetimeInMinutes,
                _ => throw new InvalidOperationException(nameof(TToken))
            };

            var token = _jwtWorker.GenerateToken(user, issuedAtUtc, TimeSpan.FromMinutes(expiresAtUtc), roles, cancellationToken);
            var tokenModel = new TToken()
            {
                ExpiresAtUtc = issuedAtUtc.AddMinutes(expiresAtUtc),
                IssuedAtUtc = issuedAtUtc,
                Token = token
            };
            return tokenModel;
        }
    }
}
