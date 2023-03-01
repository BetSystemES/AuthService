namespace AuthService.BusinessLogic.Models
{
    // TODO: Change file location to AuthService.DataAccess.Entities
    /// <summary>
    /// User refresh token
    /// </summary>
    public class UserRefreshToken
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        /// <value>
        /// The token.
        /// </value>
        public string? Token { get; set; }

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

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User? User { get; set; }
    }
}
