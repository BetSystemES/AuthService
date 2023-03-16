using Grpc.Core;
using Grpc.Net.Client.Configuration;

namespace AuthService.Grpc.Infrastructure.ConfigurationSettings
{
    /// <summary>
    /// Grpc retry policy configuration.
    /// </summary>
    public static class GrpcRetryPolicyConfiguration
    {
        /// <summary>
        /// The default method configuration
        /// </summary>
        public static readonly MethodConfig DefaultMethodConfig = new()
        {
            Names = { MethodName.Default },
            RetryPolicy = new RetryPolicy
            {
                MaxAttempts = 3,
                InitialBackoff = TimeSpan.FromSeconds(3),
                MaxBackoff = TimeSpan.FromSeconds(3),
                BackoffMultiplier = 1,
                RetryableStatusCodes =
                {
                    StatusCode.Unauthenticated,
                    StatusCode.NotFound,
                    StatusCode.Unavailable,
                }
            }
        };
    }
}
