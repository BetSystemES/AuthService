﻿using AuthService.Grpc.Extensions;
using FluentValidation;

namespace AuthService.Grpc.Infrastructure.Validators.RequestValidators
{
    /// <summary>
    /// Validator for <seealso cref="AuthenticateRequest"/>
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator&lt;AuthService.Grpc.AuthenticateRequest&gt;" />
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticateRequestValidator"/> class.
        /// </summary>
        public AuthenticateRequestValidator()
        {
            RuleFor(r => r.Password)
                .MustBeValidPassword();

            RuleFor(r => r.Email)
                .MustBeValidEmail()
                .WithMessage($"Email is invalid");
        }
    }
}
