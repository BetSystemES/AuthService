using System.ComponentModel;
using AuthService.BusinessLogic.Models.Enums;

namespace AuthService.BusinessLogic.Extensions
{
    /// <summary>
    /// Extension methods for <seealso cref="AuthRole"/>.
    /// </summary>
    public static class AuthRoleExtensions
    {
        /// <summary>
        /// Gets the description attribute.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Item not found. - role</exception>
        public static string GetDescription(this AuthRole role)
        {
            var field = role.GetType().GetField(role.ToString());
            return Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute
                ? attribute.Description
                : throw new ArgumentException("Item not found.", nameof(role));
        }
    }
}
