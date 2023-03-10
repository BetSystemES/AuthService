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
        /// Gets or sets the role ids.
        /// </summary>
        /// <value>
        /// The role ids.
        /// </value>
        public IEnumerable<Guid> RoleIds { get; set; }
    }
}
