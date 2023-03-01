namespace AuthService.BusinessLogic
{
    // TODO: typo in JWTConfig. Should be JwtConfig
    // TODO: should changed location of file to AuthService.BusinessLogic.Models.AppSettings
    /// <summary>Provides configuration for jwt generation</summary>
    public class JWTConfig
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the token lifetime in minutes.
        /// </summary>
        /// <value>
        /// The token lifetime in minutes.
        /// </value>
        public double TokenLifetimeInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the refresh token lifetime in minutes.
        /// </summary>
        /// <value>
        /// The refresh token lifetime in minutes.
        /// </value>
        public double RefreshTokenLifetimeInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the refresh token size in bytes.
        /// </summary>
        /// <value>
        /// The refresh token size in bytes.
        /// </value>
        public int RefreshTokenSizeInBytes { get; set; }
    }
}
