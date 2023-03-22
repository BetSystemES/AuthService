using FluentAssertions;
using Moq;
using AuthService.UnitTests.Infrastructure;
using AuthService.UnitTests.Infrastructure.Builders;

namespace AuthService.UnitTests.Services
{
    [Trait(Constants.Category, Constants.UnitTest)]
    public class RoleServiceTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            // Arrange
            var verifier = new RoleServiceVerifierBuilder()
                .SetRoleServiceExpectedResult()
                .SetupRoleServiceRoleProviderGetAll()
                .Build();

            // Act
            var result = await verifier.RoleService.GetAll(It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeSubsetOf(verifier.ExpectedResult);

            verifier.VerifyRoleProviderGetAll();
        }
    }
}
