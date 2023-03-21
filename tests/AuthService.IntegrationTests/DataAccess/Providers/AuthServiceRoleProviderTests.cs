using AuthService.BusinessLogic.Entities;
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
            var count = _seedRoles.Count;

            //Act
            var actualResult = await RoleProvider.GetAll(CancellationToken);

            // Assert
            actualResult.Should().Equal(_seedRoles, (c1, c2) => c1.Name == c2.Name);
            actualResult.Should().HaveCount(count, "because we have this count of seed roles");
        }

        private static readonly List<Role> _seedRoles = new()
        {
            new() { Name = AuthRole.Admin.GetDescription() },
            new() { Name = AuthRole.User.GetDescription() },
        };
    }
}
