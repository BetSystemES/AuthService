using AuthService.Grpc.Extensions;
using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="AuthenticateRequest"/>
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.AuthenticateRequest&gt;" />
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(r => r.Password)
                .MustBeValidPassword()
                .WithMessage($"Password is invalid");

            RuleFor(r => r.Email)
                .MustBeValidEmail()
                .WithMessage($"Email is invalid");
        }
    }
}
