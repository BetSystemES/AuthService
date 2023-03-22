using FluentAssertions;

namespace AuthService.IntegrationTests.DataAccess.Providers
{
    public class AuthServiceUserProviderTests : BaseTestClass
    {
        public AuthServiceUserProviderTests(GrpcAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetById_Should_Return_Result()
        {
            //Arrange
            var user = await ArrangeUser();

            //Act
            var actualResult = await UserProvider.GetById(user.Id, CancellationToken);

            // Assert
            actualResult.Should()
                .NotBeNull()
                .And.
                BeEquivalentTo(user);
        }

        [Fact]
        public async Task GetUserByEmail_Should_Return_Result()
        {
            //Arrange
            var user = await ArrangeUser();

            //Act
            var actualResult = await UserProvider.GetUserByEmail(user.Email!, CancellationToken);

            // Assert
            actualResult.Should()
                .NotBeNull()
                .And.
                BeEquivalentTo(user);
        }
    }
}
