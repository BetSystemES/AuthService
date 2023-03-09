using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Models;
using Microsoft.Extensions.Logging;

namespace AuthService.BusinessLogic.Services
{
    /// <summary>
    /// Auth service implementation.
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Services.IAuthService" />
    public class AuthService : IAuthService
    {
        private readonly IRefreshTokenProvider _refreshTokenProvider;
        private readonly IUserProvider _userProvider;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHashProvider _hashProvider;
        private readonly ILogger<AuthService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="refreshTokenProvider">The refresh token provider.</param>
        /// <param name="userProvider">The user provider.</param>
        /// <param name="tokenGenerator">The token generator.</param>
        /// <param name="refreshTokenRepository">The refresh token repository.</param>
        /// <param name="hashProvider">The hash provider.</param>
        /// <param name="logger">The logger.</param>
        public AuthService
        (
            IRefreshTokenProvider refreshTokenProvider,
            IUserProvider userProvider,
            ITokenGenerator tokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IHashProvider hashProvider,
            ILogger<AuthService> logger
        )
        {
            _refreshTokenProvider = refreshTokenProvider;
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _hashProvider = hashProvider;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<Token> Authenticate(string refreshToken, CancellationToken cancellationToken)
        {
            var userToken = await _refreshTokenProvider.GetToken(refreshToken, cancellationToken) ??
                throw new ApplicationException("Refresh token not found or expired.");

            var user = await _userProvider.GetById(userToken.UserId, cancellationToken) ??
                throw new ApplicationException("Associated user not found.");

            await _refreshTokenRepository.Remove(userToken, cancellationToken);
            var token = await _tokenGenerator.GenerateTokenAsync(user, cancellationToken);

            return token;
        }

        /// <inheritdoc/>
        public async Task<Token> Authenticate(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userProvider.GetUserByEmail(email, cancellationToken) ??
                throw new ApplicationException("User not found.");

            if (!_hashProvider.Verify(password, user.PasswordHash!))
            {
                throw new ApplicationException("Invalid password.");
            }

            var existingToken = await _refreshTokenProvider.GetByUserId(user.Id, cancellationToken);
            if (existingToken is not null)
            {
                await _refreshTokenRepository.Remove(existingToken, cancellationToken);
                _logger.LogTrace("Existing refresh token with id={existingToken.id} has been deleted.", existingToken.Id);
            }

            var token = await _tokenGenerator.GenerateTokenAsync(user, cancellationToken);

            return token;
        }
    }
}
