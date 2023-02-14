namespace AuthService.BusinessLogic.Contracts.Providers
{
    /// <summary>
    /// Data time provider contract.
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTime Now { get; }

        DateTime NowUtc { get; }
    }
}
