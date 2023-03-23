using Moq;
using AuthService.UnitTests.Infrastructure;
using AuthService.UnitTests.Infrastructure.Builders;
using FluentAssertions;

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

        [Fact]
        public async Task GetDefaultTest()
        {
            // Arrange
            var verifier = new RoleServiceVerifierBuilder()
                .SetRoleServiceExpectedResult(1)
                .SetupRoleServiceRoleProviderGetDefault()
                .Build();

            // Act
            var result = await verifier.RoleService.GetDefault(It.IsAny<CancellationToken>());

            // Assert
            result.Should().BeSubsetOf(verifier.ExpectedResult);

            verifier.VerifyRoleProviderGetDefault();
        }
    }
}
