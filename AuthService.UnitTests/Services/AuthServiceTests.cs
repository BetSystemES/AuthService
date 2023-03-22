using FluentAssertions;
using Moq;
using AuthService.UnitTests.Infrastructure;
using AuthService.UnitTests.Infrastructure.Builders;

namespace AuthService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class AuthServiceTests
    {
        [Fact]
        public async Task AuthenticateTest()
        {
            // Arrange
            var verifier = new AuthServiceVerifierBuilder()
                .SetAuthServiceUserRefreshToken()
                .SetAuthServiceUser()
                .SetAuthServiceExpectedResult()
                .SetupAuthServiceRefreshTokenProviderGetToken()
                .SetupAuthServiceUserProviderGetById()
                .SetupAuthServiceRefreshTokenRepositoryRemove()
                .SetupAuthServiceTokenGeneratorGenerateTokenAsync()
                .Build();

            // Act
            var result = await verifier.AuthService.Authenticate(It.IsAny<string>(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().Be(verifier.ExpectedResult);

            verifier
                .VerifyRefreshTokenProviderGetToken()
                .VerifyUserProviderGetById()
                .VerifyRefreshTokenRepositoryRemove()
                .VerifyTokenGeneratorGenerateTokenAsync();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AuthenticateApplicationExceptionTest(bool isUserTokenExists)
        {
            // Arrange
            var verifier = isUserTokenExists
                ? new AuthServiceVerifierBuilder()
                    .SetAuthServiceUserRefreshToken()
                    .SetupAuthServiceRefreshTokenProviderGetToken()
                    .SetupAuthServiceUserProviderGetById()
                    .Build()
                : new AuthServiceVerifierBuilder()
                    .SetupAuthServiceRefreshTokenProviderGetToken()
                    .SetupAuthServiceUserProviderGetById()
                    .Build();

            // Act
            Func<Task> result = async () => await verifier.AuthService.Authenticate(It.IsAny<string>(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().ThrowAsync<ApplicationException>();
        }

        [Fact]
        public async Task AuthenticateOverrideTest()
        {
            // Arrange
            var verifier = new AuthServiceVerifierBuilder()
                .SetAuthServiceUser()
                .SetAuthServiceExpectedResult()
                .SetupAuthServiceUserProviderGetUserByEmail()
                .SetupAuthServiceHashProvider()
                .SetupAuthServiceRefreshTokenProviderGetByUserId()
                .SetupAuthServiceTokenGeneratorGenerateTokenAsync()
                .Build();

            // Act
            var result = await verifier.AuthService.Authenticate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().Be(verifier.ExpectedResult);

            verifier
                .VerifyRefreshTokenProviderGetByUserId()
                .VerifyUserProviderGetUserByEmail()
                .VerifyHashProvider()
                .VerifyTokenGeneratorGenerateTokenAsync();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void AuthenticateOverrideApplicationExceptionTest(bool isUserExists)
        {
            // Arrange
            var verifier = isUserExists
                ? new AuthServiceVerifierBuilder()
                    .SetAuthServiceUser()
                    .SetupAuthServiceUserProviderGetUserByEmail()
                    .SetupAuthServiceHashProvider()
                    .Build()
                : new AuthServiceVerifierBuilder()
                    .SetupAuthServiceUserProviderGetUserByEmail()
                    .SetupAuthServiceHashProvider()
                    .Build();

            // Act
            Func<Task> result = async () => await verifier.AuthService.Authenticate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>());

            // Assert
            result.Should().ThrowAsync<ApplicationException>();
        }
    }
}
