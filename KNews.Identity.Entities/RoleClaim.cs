using Microsoft.AspNetCore.Identity;

namespace KNews.Identity.Entities
{
    /// <summary>
    /// Связь роль - утверждение
    /// </summary>
    public class RoleClaim : IdentityRoleClaim<int> {

        public virtual Role Role { get; set; }
    }
}
