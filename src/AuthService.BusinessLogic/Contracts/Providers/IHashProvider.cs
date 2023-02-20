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
        /// <returns>True if vefification sucess and false if vefification failed.</returns>
        bool Verify(string value, string hash);
    }
}
