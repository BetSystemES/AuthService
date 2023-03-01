using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Generators
{
    public class TokenGenerator : ITokenGenerator
    {
        private static readonly string _tokenType = "Bearer";
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IUserRolesProvider _userRolesProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IDataContext _dataContext;

        public TokenGenerator(IDateTimeProvider dateTimeProvider,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenGenerator refreshTokenGenerator,
            IUserRolesProvider userRolesProvider,
            IRefreshTokenRepository refreshTokenRepository,
            IDataContext dataContext)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _userRolesProvider = userRolesProvider;
            _refreshTokenRepository = refreshTokenRepository;
            _dataContext = dataContext;
        }

        public async Task<Token> GenerateTokenAsync(User user, CancellationToken cancellationToken)
        {
            var userRoles = await _userRolesProvider.GetUserRoles(user.Id, cancellationToken);
            var issuedAtUtc = _dateTimeProvider.NowUtc;
            // TODO: we can use one instance of token generator
            var jwtModel = _jwtTokenGenerator.Generate(user, issuedAtUtc, cancellationToken);
            var refreshToken = _refreshTokenGenerator.Generate(user, issuedAtUtc, cancellationToken);

            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                ExpiresAtUtc = refreshToken.ExpiresAtUtc,
                IssuedAtUtc = refreshToken.IssuedAtUtc,
                Token = refreshToken.Token
            };

            await _refreshTokenRepository.Add(userRefreshToken, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);

            return new Token
            {
                AccessToken = jwtModel.Token,
                RefreshToken = refreshToken.Token,
                IssuedAtUtc = issuedAtUtc,
                JwtTokenExpiresAtUtc = jwtModel.ExpiresAtUtc,
                RefreshTokenExpiresAtUtc = refreshToken.ExpiresAtUtc,
                Type = _tokenType
            };
        }
    }
}
