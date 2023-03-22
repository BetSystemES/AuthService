using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Entities;
using AuthService.BusinessLogic.Models;

namespace AuthService.BusinessLogic.Services
{
    /// <summary>
    /// User service implementation
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Services.IUserService" />
    public class UserService : IUserService
    {
        private readonly IUserProvider _userProvider;
        private readonly IHashProvider _hashProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserRepository _userRepository;
        private readonly IDataContext _dataContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userProvider">The user provider.</param>
        /// <param name="hashProvider">The hash provider.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="dataContext">The data context.</param>
        public UserService
        (
            IUserProvider userProvider,
            IHashProvider hashProvider,
            IDateTimeProvider dateTimeProvider,
            IUserRepository userRepository,
            IDataContext dataContext
        )
        {
            _userProvider = userProvider;
            _hashProvider = hashProvider;
            _dateTimeProvider = dateTimeProvider;
            _userRepository = userRepository;
            _dataContext = dataContext;
        }

        /// <inheritdoc/>
        public async Task<User> CreateUser(CreateUserModel model, CancellationToken cancellationToken)
        {
            var now = _dateTimeProvider.NowUtc;

            var user = new User
            {
                Email = model.Email!,
                PasswordHash = _hashProvider.Hash(model.Password!),
                CreatedAtUtc = now,
                UpdatedAtUtc = now,
            };

            await _userRepository.Add(user, cancellationToken);
            user.UserRole.AddRange(model.RoleIds
                .Select(x => new UserRole() { RoleId = x }));

            await _dataContext.SaveChanges(cancellationToken);

            return user;
        }

        /// <inheritdoc/>
        public async Task<UserSimpleModel> GetUserSimpleModel(Guid userId, CancellationToken token)
        {
            var user = await _userProvider.GetById(userId, token);

            return user == null
                ? throw new ApplicationException("User not found")
                : new UserSimpleModel
                {
                    Email = user.Email,
                    Id = userId,
                    IsLocked = false
                };
        }

        /// <inheritdoc/>
        public async Task Remove(User user, CancellationToken cancellationToken)
        {
            await _userRepository.Remove(user, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task Remove(Guid userId, string email, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Id = userId,
                Email = email
            };

            await _userRepository.Remove(user, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);
        }
    }
}
