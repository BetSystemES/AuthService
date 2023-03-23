using FizzWare.NBuilder;
using Moq;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Services;
using AuthService.UnitTests.Infrastructure.Verifiers;

namespace AuthService.UnitTests.Infrastructure.Builders
{
    public class RoleServiceVerifierBuilder
    {
        private readonly Mock<IRoleProvider> _roleProvider;

        private List<Role>? _expectedResult;

        private readonly RoleService _roleService;

        public RoleServiceVerifierBuilder()
        {
            _roleProvider = new();
            _roleService = new(_roleProvider.Object);
        }

        public RoleServiceVerifierBuilder SetRoleServiceExpectedResult(int size = 3)
        {
            _expectedResult = Builder<Role>
                .CreateListOfSize(size)
                .Build()
                .ToList();

            return this;
        }

        public RoleServiceTestsVerifier Build()
        {
            return new RoleServiceTestsVerifier(
                _roleService,
                _roleProvider,
                _expectedResult);
        }

        public RoleServiceVerifierBuilder SetupRoleServiceRoleProviderGetAll()
        {
            _roleProvider.Setup(x => x.GetAll(It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedResult)
                .Verifiable();

            return this;
        }

        public RoleServiceVerifierBuilder SetupRoleServiceRoleProviderGetDefault()
        {
            _roleProvider.Setup(x => x.GetDefault(It.IsAny<CancellationToken>()))
                .ReturnsAsync(_expectedResult)
                .Verifiable();

            return this;
        }
    }
}
