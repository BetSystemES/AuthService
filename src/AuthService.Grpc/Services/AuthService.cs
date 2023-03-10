using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Models;
using AutoMapper;
using Grpc.Core;

namespace AuthService.Grpc.Services
{
    /// <summary>
    /// Auth grpc service implementation.
    /// </summary>
    /// <seealso cref="AuthService.Grpc.AuthService.AuthServiceBase" />
    public class AuthService : Grpc.AuthService.AuthServiceBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly ILogger<AuthService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="authService">The authentication service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public AuthService(IUserService userService,
            IAuthService authService,
            IMapper mapper,
            IRoleService roleService,
            ILogger<AuthService> logger)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns>AuthenticateResponse</returns>
        public override async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, ServerCallContext context)
        {
            var cancellationToken = context.CancellationToken;
            var token = await _authService.Authenticate(request.Email, request.Password, cancellationToken);

            _logger.LogTrace("Authentication is completed. Auth token will expire: {Date}", token.JwtTokenExpiresAtUtc);

            return new AuthenticateResponse { Token = _mapper.Map<Token>(token) };
        }

        /// <summary>
        /// Refreshes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns>RefreshResponse</returns>
        public override async Task<RefreshResponse> Refresh(RefreshRequest request, ServerCallContext context)
        {
            var cancellationToken = context.CancellationToken;
            var token = await _authService.Authenticate(request.RefreshToken, cancellationToken);

            _logger.LogTrace("Refresh token is completed. Auth token will expire: {Date}", token.JwtTokenExpiresAtUtc);

            return new RefreshResponse { Token = _mapper.Map<Token>(token) };
        }

        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns>CreateUserResponse</returns>
        public override async Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;
            var createuserModel = _mapper.Map<CreateUserModel>(request);
            var user = await _userService.CreateUser(
                createuserModel,
                token);

            var response = new CreateUserResponse()
            {
                User = _mapper.Map<User>(user)
            };

            return response;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns>GetUserResponse</returns>
        public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.UserId, out var userId))
            {
                _logger.LogTrace("Invalid UserId: {Id}", request.UserId);
                throw new RpcException(new Status(StatusCode.InvalidArgument, "UserId should be Guid."));
            }

            var token = context.CancellationToken;
            var user = await _userService.GetUserSimpleModel(userId, token);

            return new GetUserResponse { User = _mapper.Map<User>(user) };
        }

        public override Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            return base.DeleteUser(request, context);
        }

        public override async Task<GetAllRolesResponse> GetAllRoles(GetAllRolesRequest request, ServerCallContext context)
        {
            var token = context.CancellationToken;

            var roles = await _roleService.GetAll(token);

            var grpcRoles = _mapper.Map<IEnumerable<Role>>(roles);

            var response = new GetAllRolesResponse();
            response.Roles.AddRange(grpcRoles);

            return response;
        }
    }
}
