namespace AuthService.BusinessLogic.Models
{
    // TODO: Change file location to AuthService.BusinessLogic.Entities
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            UserRole = new List<UserRole>();
            RefreshTokens = new List<UserRefreshToken>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>
        /// The password hash.
        /// </value>
        public string? PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the lockout end at UTC.
        /// </summary>
        /// <value>
        /// The lockout end at UTC.
        /// </value>
        public DateTime? LockoutEndAtUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [lockout enabled].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [lockout enabled]; otherwise, <c>false</c>.
        /// </value>
        public bool LockoutEnabled { get; set; }

        /// <summary>
        /// Gets or sets the access failed count.
        /// </summary>
        /// <value>
        /// The access failed count.
        /// </value>
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// Gets or sets the created at UTC.
        /// </summary>
        /// <value>
        /// The created at UTC.
        /// </value>
        public DateTime CreatedAtUtc { get; set; }

        /// <summary>
        /// Gets or sets the updated at UTC.
        /// </summary>
        /// <value>
        /// The updated at UTC.
        /// </value>
        public DateTime? UpdatedAtUtc { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        /// <value>
        /// The user role.
        /// </value>
        public List<UserRole> UserRole { get; set; }

        /// <summary>
        /// Gets or sets the refresh tokens.
        /// </summary>
        /// <value>
        /// The refresh tokens.
        /// </value>
        public List<UserRefreshToken> RefreshTokens { get; set; }
    }
}
