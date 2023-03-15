using FluentValidation;

namespace AuthService.Grpc.Extensions
{
    /// <summary>
    ///  Validation rule for email.
    /// </summary>
    public static class EmailValidationRule
    {
        /// <summary>
        /// Musts the be valid email.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, string> MustBeValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .EmailAddress();

            return builderOptions;
        }
    }
}
