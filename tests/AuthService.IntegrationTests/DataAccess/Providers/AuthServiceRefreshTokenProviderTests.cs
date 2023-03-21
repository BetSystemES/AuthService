using AuthService.BusinessLogic.Entities;
using FluentAssertions;

namespace AuthService.IntegrationTests.DataAccess.Providers
{
    public class AuthServiceRefreshTokenProviderTests : BaseTestClass
    {
        public AuthServiceRefreshTokenProviderTests(GrpcAppFactory factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("")]
        public async Task GetToken_Should_Return_Null(string refreshToken)
        {
            // Arrange
            var expectedResult = default(UserRefreshToken);

            // Act
            var actualResult = await RefreshTokenProvider.GetToken(refreshToken, CancellationToken);

            // Assert
            actualResult.Should()
                .BeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetToken_Should_Return_Token()
        {
            //Arrange
            var user = await ArrangeUser(true);
            var expectedResult = await AuthService.Authenticate(user.Email, user.PasswordHash, CancellationToken);
            var refreshToken = expectedResult.AccessToken;

            // Act
            var actualResult = await RefreshTokenProvider.GetToken(refreshToken, CancellationToken);

            //Assert
            actualResult.Should()
                .NotBeNull().And
                .BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetUserById_Should_Return_Result()
        {
            //Arrange
            var user = await ArrangeUser();

            //Act
            var actualResult = await RefreshTokenProvider.GetByUserId(user.Id, CancellationToken);

            // Assert
            actualResult.Should()
                .NotBeNull();
        }
    }
}
