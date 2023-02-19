namespace AuthService.BusinessLogic.Contracts.Providers
{
    /// <summary>
    /// Data time provider contract.
    /// </summary>
    public interface IDateTimeProvider
    {
        /// <summary>
        /// Gets the now.
        /// </summary>
        /// <value>
        /// The now DateTime.
        /// </value>
        DateTime Now { get; }

        /// <summary>
        /// Gets the now UTC.
        /// </summary>
        /// <value>
        /// The now UTC DateTime.
        /// </value>
        DateTime NowUtc { get; }
    }
}
