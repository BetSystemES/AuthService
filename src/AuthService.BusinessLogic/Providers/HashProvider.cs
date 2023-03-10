using AuthService.BusinessLogic.Contracts.Providers;

namespace AuthService.BusinessLogic.Providers
{
    /// <summary>
    /// Has provider implementation.
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Providers.IHashProvider" />
    public class HashProvider : IHashProvider
    {
        /// <inheritdoc/>
        public string Hash(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

        /// <inheritdoc/>
        public bool Verify(string value, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(value, hash);
        }
    }
}
