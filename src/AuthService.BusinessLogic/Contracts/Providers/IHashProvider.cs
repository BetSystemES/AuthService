namespace AuthService.BusinessLogic.Contracts.Providers
{
    /// <summary>
    /// Hash provider contract.
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Hashes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Hashed string.</returns>
        string Hash(string value);

        /// <summary>
        /// Verifies the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>True if verification success and false if verification failed.</returns>
        bool Verify(string value, string hash);
    }
}
