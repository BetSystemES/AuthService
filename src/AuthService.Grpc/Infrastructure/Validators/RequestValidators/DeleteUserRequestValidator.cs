using AuthService.Grpc.Extensions;
using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="DeleteUserRequest"/>
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.DeleteUserRequest&gt;" />
    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteUserRequestValidator"/> class.
        /// </summary>
        public DeleteUserRequestValidator()
        {
            RuleFor(e => e.UserId)
                .MustBeValidGuid()
                .WithMessage($"Guid is invalid");
        }
    }
}
