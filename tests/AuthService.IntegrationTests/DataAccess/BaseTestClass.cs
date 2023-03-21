using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Generators;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Extensions;
using AuthService.BusinessLogic.Models;
using AuthService.BusinessLogic.Models.Enums;
using FizzWare.NBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.IntegrationTests.DataAccess
{
    public class BaseTestClass : IClassFixture<GrpcAppFactory>, IDisposable
    {
        protected static readonly CancellationToken CancellationToken = CancellationToken.None;

        protected readonly IServiceScope Scope;
        protected readonly IRefreshTokenRepository RefreshTokenRepository;
        protected readonly IRefreshTokenProvider RefreshTokenProvider;
        protected readonly IDateTimeProvider DateTimeProvider;
        protected readonly IUserRepository UserRepository;
        protected readonly IRoleProvider RoleProvider;
        protected readonly IUserProvider UserProvider;
        protected readonly IUserRolesProvider UserRolesProvider;
        protected readonly IDataContext DataContext;
        protected readonly ITokenGenerator TokenGenerator;
        protected readonly IJwtTokenGenerator JwtTokenGenerator;
        protected readonly IUserService UserService;
        protected readonly IHashProvider HashProvider;
        protected readonly IAuthService AuthService;

        public BaseTestClass(GrpcAppFactory factory)
        {
            Scope = factory.Services.CreateScope();
            RefreshTokenRepository = factory.Services.GetRequiredService<IRefreshTokenRepository>();
            RefreshTokenProvider = factory.Services.GetRequiredService<IRefreshTokenProvider>();
            DateTimeProvider = factory.Services.GetRequiredService<IDateTimeProvider>();
            UserRepository = factory.Services.GetRequiredService<IUserRepository>();
            RoleProvider = factory.Services.GetRequiredService<IRoleProvider>();
            UserProvider = factory.Services.GetRequiredService<IUserProvider>();
            UserRolesProvider = factory.Services.GetRequiredService<IUserRolesProvider>();
            DataContext = Scope.ServiceProvider.GetRequiredService<IDataContext>();
            TokenGenerator = Scope.ServiceProvider.GetRequiredService<ITokenGenerator>();
            JwtTokenGenerator = Scope.ServiceProvider.GetRequiredService<IJwtTokenGenerator>();
            UserService = Scope.ServiceProvider.GetRequiredService<IUserService>();
            HashProvider = Scope.ServiceProvider.GetRequiredService<IHashProvider>();
            AuthService = Scope.ServiceProvider.GetRequiredService<IAuthService>();
        }

        protected async Task<User> ArrangeUser(bool isOpenPassword = false)
        {
            var roles = await RoleProvider.GetAll(CancellationToken);

            var createUserModel = Builder<CreateUserModel>
                .CreateNew()
                .With(x => x.RoleIds = roles.Select(x => x.Id))
                .Build();

            var user = await UserProvider.GetUserByEmail(createUserModel.Email, CancellationToken) ??
                await UserService.CreateUser(createUserModel, CancellationToken);

            if (isOpenPassword)
            {
                user.PasswordHash = createUserModel.Password;
            }
            return user;
        }

        protected static readonly List<Role> SeedRoles = new()
        {
            new() { Name = AuthRole.Admin.GetDescription() },
            new() { Name = AuthRole.User.GetDescription() },
        };

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}
