using FluentValidation;

namespace AuthService.Grpc.Extensions
{
    /// <summary>
    /// Validation rule for pawwsord.
    /// </summary>
    public static class PasswordValidationRule
    {
        /// <summary>
        /// Musts the be valid password.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, string> MustBeValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .NotNull()
                .NotEmpty();

            return builderOptions;
        }
    }
}
