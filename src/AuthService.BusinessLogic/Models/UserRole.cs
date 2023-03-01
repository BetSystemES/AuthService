namespace AuthService.BusinessLogic.Models
{
    // TODO: Change file location to new dll project AuthService.DataAccess.Entities
    /// <summary>
    /// User role many-to-many entity
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User? User { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public Role? Role { get; set; }
    }
}
