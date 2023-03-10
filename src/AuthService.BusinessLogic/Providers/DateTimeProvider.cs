using AuthService.BusinessLogic.Contracts.Providers;

namespace AuthService.BusinessLogic.Providers
{
    /// <summary>
    /// Date time provider implementation
    /// </summary>
    /// <seealso cref="IDateTimeProvider" />
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;

        public DateTime NowUtc => DateTime.UtcNow;
    }
}
