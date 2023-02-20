using Grpc.Core;

namespace AuthService.Grpc.Services
{
    public class AuthService : Grpc.AuthService.AuthServiceBase
    {
        public AuthService()
        {

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
        public override Task<CreateUserResponse> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            return base.CreateUser(request, context);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="context">The context.</param>
        /// <returns>GetUserResponse</returns>
        public override Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
        {
            return base.GetUser(request, context);
        }

        public override Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
        {
            return base.DeleteUser(request, context);
        }
    }
}
