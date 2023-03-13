using System.Runtime.InteropServices.ObjectiveC;
using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="RefreshResponse"/>.
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.RefreshResponse&gt;" />
    public class RefreshResponseValidator : AbstractValidator<RefreshResponse>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefreshResponseValidator"/> class.
        /// </summary>
        public RefreshResponseValidator()
        {
            RuleFor(r => r.Token)
                .NotNull()
                .NotEmpty()
                .WithMessage($"Refresh token is invalid");
        }
    }
}
