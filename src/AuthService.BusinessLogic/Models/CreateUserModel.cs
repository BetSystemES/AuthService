namespace AuthService.BusinessLogic.Models
{
    /// <summary>
    /// Create user model
    /// </summary>
    public class CreateUserModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string? Password { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public List<string>? RoleNames { get; set; }

        /// <summary>
        /// Gets or sets the claims.
        /// </summary>
        /// <value>
        /// The claims.
        /// </value>
        public Dictionary<string, string>? Claims { get; set; }

        /// <summary>
        /// Gets or sets the role ids.
        /// </summary>
        /// <value>
        /// The role ids.
        /// </value>
        public IEnumerable<Guid> RoleIds { get; set; }
    }
}
