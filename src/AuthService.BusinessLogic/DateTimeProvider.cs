using AuthService.BusinessLogic.Contracts.Providers;

namespace AuthService.BusinessLogic
{
    // TODO: should changed location of file to AuthService.BusinessLogic.Providers
    /// <summary>
    /// Date time provider implementation
    /// </summary>
    /// <seealso cref="AuthService.BusinessLogic.Contracts.Providers.IDateTimeProvider" />
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;

        public DateTime NowUtc => DateTime.UtcNow;
    }
}
