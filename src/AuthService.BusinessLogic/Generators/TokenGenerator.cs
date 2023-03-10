using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Generators
{
    /// <summary>
    /// Token generator implementation. Token=<seealso cref="Token"/>
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Generators.ITokenGenerator" />
    public class TokenGenerator : ITokenGenerator
    {
        private static readonly string _tokenType = "Bearer";
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRolesProvider _userRolesProvider;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IDataContext _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenGenerator"/> class.
        /// </summary>
        /// <param name="dateTimeProvider">The date time provider.</param>
        /// <param name="jwtTokenGenerator">The JWT token generator.</param>
        /// <param name="userRolesProvider">The user roles provider.</param>
        /// <param name="refreshTokenRepository">The refresh token repository.</param>
        /// <param name="dataContext">The data context.</param>
        public TokenGenerator(IDateTimeProvider dateTimeProvider,
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRolesProvider userRolesProvider,
            IRefreshTokenRepository refreshTokenRepository,
            IDataContext dataContext)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRolesProvider = userRolesProvider;
            _refreshTokenRepository = refreshTokenRepository;
            _dataContext = dataContext;
        }

        /// <inheritdoc/>
        public async Task<Token> GenerateTokenAsync(User user, CancellationToken cancellationToken)
        {
            var userRoles = await _userRolesProvider.GetUserRoles(user.Id, cancellationToken);
            var issuedAtUtc = _dateTimeProvider.NowUtc;
            var accessToken = _jwtTokenGenerator.Generate<AccessToken>(user, issuedAtUtc, cancellationToken);
            var refreshToken = _jwtTokenGenerator.Generate<RefreshToken>(user, issuedAtUtc, cancellationToken);

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
                AccessToken = accessToken.Token,
                RefreshToken = refreshToken.Token,
                IssuedAtUtc = issuedAtUtc,
                JwtTokenExpiresAtUtc = accessToken.ExpiresAtUtc,
                RefreshTokenExpiresAtUtc = refreshToken.ExpiresAtUtc,
                Type = _tokenType
            };
        }
    }
}
