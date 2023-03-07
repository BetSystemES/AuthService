using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;
using AuthService.BusinessLogic.Models.AppSettings;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic.Generators
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtConfig _jWtConfig;
        private readonly IJwtWorker _jWtWorker;

        public JwtTokenGenerator(IOptions<JwtConfig> jWTConfig, IJwtWorker jWTWorker)
        {
            _jWtConfig = jWTConfig.Value;
            _jWtWorker = jWTWorker;
        }

        public TToken Generate<TToken>(User user, DateTime issuedAtUtc, CancellationToken cancellationToken) where TToken : JwtToken
        {
            var expiresAtUtc = nameof(TToken) switch
            {
                nameof(RefreshToken) => _jWtConfig.RefreshTokenLifetimeInMinutes,
                nameof(AccessToken) => _jWtConfig.TokenLifetimeInMinutes,
                _ => throw new InvalidOperationException(nameof(TToken))
            };

            var token = _jWtWorker.GenerateToken(
                user,
                issuedAtUtc,
                TimeSpan.FromMinutes(expiresAtUtc),
                cancellationToken);

            var tokenModel = new JwtToken()
            {
                ExpiresAtUtc = issuedAtUtc.AddMinutes(expiresAtUtc),
                IssuedAtUtc = issuedAtUtc,
                Token = token
            };

            return (TToken)tokenModel;
        }
    }
}
