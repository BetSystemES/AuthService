using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Services;

namespace AuthService.UnitTests.Infrastructure.Verifiers
{
    public class RoleServiceTestsVerifier
    {
        private readonly Mock<IRoleProvider> _roleProvider;

        public readonly List<Role> ExpectedResult;

        public readonly RoleService RoleService;

        public RoleServiceTestsVerifier(
            RoleService roleService,
            Mock<IRoleProvider> roleProvider,
            List<Role> expectedResult)
        {
            _roleProvider = roleProvider;
            RoleService = roleService;
            ExpectedResult = expectedResult;
        }

        public RoleServiceTestsVerifier VerifyRoleProviderGetAll()
        {
            _roleProvider.Verify(x => x.GetAll(It.IsAny<CancellationToken>()), Times.Once);

            return this;
        }
    }
}
