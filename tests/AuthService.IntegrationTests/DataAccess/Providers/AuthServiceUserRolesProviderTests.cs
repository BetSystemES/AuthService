using FluentAssertions;

namespace AuthService.IntegrationTests.DataAccess.Providers
{
    public class AuthServiceUserRolesProviderTests : BaseTestClass
    {
        public AuthServiceUserRolesProviderTests(GrpcAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAll_Should_Return_Result()
        {
            //Arrange
            var user = await ArrangeUser();

            //Act
            var actualResult = await UserRolesProvider.GetUserRoles(user.Id, CancellationToken);

            // Assert
            actualResult.Should().Equal(SeedRoles, (c1, c2) => c1.Name == c2.Name);
            actualResult.Should().HaveCount(SeedRoles.Count, "because we have this count of seed roles");
        }
    }
}
