using Microsoft.Extensions.Logging;
using FizzWare.NBuilder;
using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.UnitTests.Infrastructure.Verifiers;
using Token = AuthService.BusinessLogic.Models.Token;
using User = AuthService.BusinessLogic.Entities.User;

namespace AuthService.UnitTests.Infrastructure.Builders
{
    public class AuthServiceVerifierBuilder
    {
        private readonly Mock<IRefreshTokenProvider> _refreshTokenProvider;
        private readonly Mock<IUserProvider> _userProvider;
        private readonly Mock<ITokenGenerator> _tokenGenerator;
        private readonly Mock<IRefreshTokenRepository> _refreshTokenRepository;
        private readonly Mock<IHashProvider> _hashProvider;
        private readonly Mock<ILogger<BusinessLogic.Services.AuthService>> _logger;

        private UserRefreshToken? _userToken;
        private User? _user;
        private Token? _expectedResult;

        private readonly BusinessLogic.Services.AuthService _authService;

        public AuthServiceVerifierBuilder()
        {
            _refreshTokenProvider = new();
            _userProvider = new();
            _tokenGenerator = new();
            _refreshTokenRepository = new();
            _logger = new();
            _hashProvider = new();

            _authService = new(
                _refreshTokenProvider.Object,
                _userProvider.Object,
                _tokenGenerator.Object,
                _refreshTokenRepository.Object,
                _hashProvider.Object,
                _logger.Object);
        }

        public AuthServiceVerifierBuilder AddAuthServiceUserToken()
        {
            _userToken = Builder<UserRefreshToken>
                .CreateNew()
                .Build();

            return this;
        }

        public AuthServiceVerifierBuilder AddAuthServiceUser()
        {
            _user = Builder<User>
                .CreateNew()
                .Build();

            return this;
        }

        public AuthServiceVerifierBuilder AddAuthServiceExpectedResult()
        {
            _expectedResult = Builder<Token>
                .CreateNew()
            .Build();

            return this;
        }

        public AuthServiceTestsVerifier Build()
        {
            return new AuthServiceTestsVerifier(
                _hashProvider,
                _refreshTokenProvider,
                _userProvider,
                _tokenGenerator,
                _refreshTokenRepository,
                _authService,
                _expectedResult);
        }

        public AuthServiceVerifierBuilder SetupAuthServiceRefreshTokenProviderGetToken()
        {
            _refreshTokenProvider.Setup(x => x.GetToken(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_userToken)
                .Verifiable();

            return this;
        }

        public AuthServiceVerifierBuilder SetupAuthServiceRefreshTokenProviderGetByUserId()
        {
            _refreshTokenProvider.Setup(x => x.GetByUserId(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(default(UserRefreshToken))
                .Verifiable();

            return this;
        }

        public AuthServiceVerifierBuilder SetupAuthServiceUserProviderGetById()
        {
            _userProvider.Setup(x => x.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_user)
                .Verifiable();

            return this;
        }

        public AuthServiceVerifierBuilder SetupAuthServiceUserProviderGetUserByEmail()
        {
            _userProvider.Setup(x => x.GetUserByEmail(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_user)
                .Verifiable();

            return this;
        }

        public AuthServiceVerifierBuilder SetupAuthServiceRefreshTokenRepositoryRemove()
        {
            _refreshTokenRepository.Setup(x => x.Remove(_userToken, It.IsAny<CancellationToken>()))
                .Verifiable();

            return this;
        }

        public AuthServiceVerifierBuilder SetupAuthServiceTokenGeneratorGenerateTokenAsync()
        {
            _tokenGenerator.Setup(x => x.GenerateTokenAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedResult)
                .Verifiable();

            return this;
        }

        public AuthServiceVerifierBuilder SetupAuthServiceHashProvider()
        {
            _hashProvider.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true)
                .Verifiable();

            return this;
        }
    }
}
