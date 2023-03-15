using FluentValidation;

namespace AuthService.Grpc.Extensions
{
    /// <summary>
    /// Validation rule for guid
    /// </summary>
    public static class ValidationRulesExtensions
    {
        /// <summary>
        /// Must the be valid unique identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, string> MustBeValidGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .NotEmpty()
                .Must(e => Guid.TryParse(e, out var guid));

            return builderOptions;
        }

        /// <summary>
        /// Musts the be valid unique identifier enumerable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder">The rule builder.</param>
        /// <returns>IRuleBuilderOptions</returns>
        public static IRuleBuilderOptions<T, IEnumerable<string>> MustBeValidGuidEnumerable<T>(this IRuleBuilder<T, IEnumerable<string>> ruleBuilder)
        {
            var builderOptions = ruleBuilder
                .NotEmpty()
                .Must(e => e.All(x => Guid.TryParse(x, out var guid)));

            return builderOptions;
        }
    }
}
