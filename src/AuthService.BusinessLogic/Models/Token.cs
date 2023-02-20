namespace AuthService.BusinessLogic.Models
{
    /// <summary>
    /// Token entity
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string? AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>
        /// The type of the token.
        /// </value>
        public string? Type { get; set; }

        /// <summary>
        /// Gets the issued at UTC.
        /// </summary>
        /// <value>
        /// The issued at UTC.
        /// </value>
        public DateTime IssuedAtUtc { get; init; }

        /// <summary>
        /// Gets the JWT token expires at UTC.
        /// </summary>
        /// <value>
        /// The JWT token expires at UTC.
        /// </value>
        public DateTime JwtTokenExpiresAtUtc { get; init; }

        /// <summary>
        /// Gets the refresh token expires at UTC.
        /// </summary>
        /// <value>
        /// The refresh token expires at UTC.
        /// </value>
        public DateTime RefreshTokenExpiresAtUtc { get; init; }
    }
}
