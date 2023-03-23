using AuthService.BusinessLogic.Extensions;
using AuthService.BusinessLogic.Models.Enums;
using FluentAssertions;

namespace AuthService.IntegrationTests.DataAccess.Providers
{
    public class AuthServiceRoleProviderTests : BaseTestClass
    {
        public AuthServiceRoleProviderTests(GrpcAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAll_Should_Return_Result()
        {
            //Arrange
            var count = SeedRoles.Count;

            //Act
            var actualResult = await RoleProvider.GetAll(CancellationToken);

            // Assert
            actualResult.Should().Equal(SeedRoles, (c1, c2) => c1.Name == c2.Name);
            actualResult.Should().HaveCount(count, "because we have this count of seed roles");
        }

        [Fact]
        public async Task GetDefault_Should_Return_DefaultResult()
        {
            //Arrange
            var defaultName = AuthRole.User.GetDescription();
            var defaultRoles = SeedRoles.Where(x => string.Equals(x.Name, defaultName));

            //Act
            var actualResult = await RoleProvider.GetDefault(CancellationToken);

            // Assert
            actualResult.Should().Equal(defaultRoles, (c1, c2) => c1.Name == c2.Name);
        }
    }
}
