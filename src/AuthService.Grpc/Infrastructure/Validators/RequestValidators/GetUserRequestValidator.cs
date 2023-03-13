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
        public GetUserRequestValidator()
        {
            RuleFor(r => r.UserId)
                .MustBeValidGuid()
                .WithMessage($"Guid is invalid");
        }
    }
}
