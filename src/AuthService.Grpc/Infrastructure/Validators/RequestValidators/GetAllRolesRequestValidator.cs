using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="GetAllRolesRequestValidator" />
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.GetAllRolesRequest&gt;" />
    public class GetAllRolesRequestValidator : AbstractValidator<GetAllRolesRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllRolesRequestValidator"/> class.
        /// </summary>
        public GetAllRolesRequestValidator()
        {
        }
    }
}
