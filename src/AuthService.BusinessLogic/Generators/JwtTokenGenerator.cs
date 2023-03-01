using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Worker;
using AuthService.BusinessLogic.Models;
using Microsoft.Extensions.Options;

namespace AuthService.BusinessLogic.Generators
{
    // TODO: typo in _jWTConfig. Should be JwtTokenGenerator
    public class JWTTokenGenerator : IJwtTokenGenerator
    {
        // TODO: typo in _jWTConfig. Should be _jWtConfig
        // TODO: typo in _jWTWorker. Should be _jWtWorker
        private readonly JWTConfig _jWTConfig;
        private readonly IJWTWorker _jWTWorker;

        // TODO: typo in _jWTConfig. Should be _jWtConfig
        // TODO: typo in _jWTWorker. Should be _jWtWorker
        public JWTTokenGenerator(IOptions<JWTConfig> jWTConfig, IJWTWorker jWTWorker)
        {
            _jWTConfig = jWTConfig.Value;
            _jWTWorker = jWTWorker;
        }

        public JwtToken Generate(User user, DateTime issuedAtUtc, CancellationToken cancellationToken)
        {
            var expiresAtUtc = issuedAtUtc.AddMinutes(_jWTConfig.TokenLifetimeInMinutes);

            var token = _jWTWorker.GenerageToken(
                user,
                issuedAtUtc,
                TimeSpan.FromMinutes(_jWTConfig.TokenLifetimeInMinutes),
                cancellationToken);

            var tokenModel = new JwtToken()
            {
                ExpiresAtUtc = expiresAtUtc,
                IssuedAtUtc = issuedAtUtc,
                Token = token
            };

            return tokenModel;
        }
    }
}
