namespace AuthService.BusinessLogic.Models
{
    /// <summary>
    /// Jwt token model.
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the issued at UTC.
        /// </summary>
        /// <value>
        /// The issued at UTC.
        /// </value>
        public DateTime IssuedAtUtc { get; set; }

        /// <summary>
        /// Gets or sets the expires at UTC.
        /// </summary>
        /// <value>
        /// The expires at UTC.
        /// </value>
        public DateTime ExpiresAtUtc { get; set; }
    }
}
