using AuthService.Grpc.Extensions;
using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="CreateUserRequest"/>
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.CreateUserRequest&gt;" />
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
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
