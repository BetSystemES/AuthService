using AuthService.BusinessLogic.Contracts.Services;
using AuthService.BusinessLogic.Models;
using AutoMapper;
using Grpc.Core;

namespace AuthService.Grpc.Services
{
    public class AuthService : Grpc.AuthService.AuthServiceBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserService userService,
            IAuthService authService,
            IMapper mapper,
            ILogger<AuthService> logger)
        {
            _userService = userService;
            _authService = authService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns>AuthenticateResponse</returns>
        public override Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, ServerCallContext context)
        {
            return base.Authenticate(request, context);
        }

        /// <summary>
        /// Refreshes the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns>RefreshResponse</returns>
        public override Task<RefreshResponse> Refresh(RefreshRequest request, ServerCallContext context)
        {
            return base.Refresh(request, context);
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

            var user = await _userService.CreateUser(
                _mapper.Map<CreateUserModel>(request),
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
    }
}
