using AuthService.Grpc.Extensions;
using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="GetUserRequest"/>
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.GetUserRequest&gt;" />
    public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserRequestValidator"/> class.
        /// </summary>
        public GetUserRequestValidator()
        {
            RuleFor(r => r.UserId)
                .MustBeValidGuid()
                .WithMessage($"Guid is invalid");
        }
    }
}
