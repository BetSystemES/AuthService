using AuthService.BusinessLogic.Contracts.DataAccess;
using AuthService.BusinessLogic.Contracts.DataAccess.Providers;
using AuthService.BusinessLogic.Contracts.DataAccess.Repositories;
using AuthService.BusinessLogic.Contracts.Providers;
using AuthService.BusinessLogic.Contracts.Services;
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

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// User simplified model
        /// </returns>
        public async Task<UserSimpleModel> CreateUser(CreateUserModel model, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = model.Email!,
                PasswordHash = _hashProvider.Hash(model.Password!),
                CreatedAtUtc = _dateTimeProvider.NowUtc,
                UpdatedAtUtc = _dateTimeProvider.NowUtc,
                UserRole = new List<UserRole>(),
            };

            await _userRepository.Add(user, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);

            return new UserSimpleModel
            {
                Email = user.Email,
                Id = user.Id,
                IsLocked = user.LockoutEnabled
            };
        }

        /// <summary>
        /// Gets the user simple model.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns>
        /// User simplified model
        /// </returns>
        /// <exception cref="System.ApplicationException">User not found</exception>
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
    }
}
