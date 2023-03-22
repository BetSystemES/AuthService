using System.Text;

namespace AuthService.Grpc.Extensions
{
    /// <summary>
    /// Extensions method for <seealso cref="Exception"/>.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets all exeption messages including inner expetions.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">ex</exception>
        public static string GetAllExceptionMessages(this Exception ex)
        {
            ArgumentNullException.ThrowIfNull(ex, nameof(ex));

            var sb = new StringBuilder();

            while (ex != null)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(" ");
                    }

                    sb.Append(ex.Message);
                }

                ex = ex.InnerException;
            }

            return sb.ToString();
        }
    }
}
