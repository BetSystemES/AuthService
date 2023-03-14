using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="RefreshRequest"/>.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.RefreshResponse&gt;" />
    public class RefreshRequestValidator : AbstractValidator<RefreshRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshRequestValidator"/> class.
        /// </summary>
        public RefreshRequestValidator()
        {
            RuleFor(r => r.RefreshToken)
                .NotNull()
                .NotEmpty()
                .WithMessage($"Refresh token is invalid");
        }
    }
}
