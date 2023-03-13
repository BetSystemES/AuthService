using System.ComponentModel;

namespace AuthService.BusinessLogic.Models.Enums
{
    /// <summary>
    /// Represent auth roles.
    /// </summary>
    public enum AuthRole
    {
        [Description("admin")]
        Admin,

        [Description("user")]
        User
    }
}
