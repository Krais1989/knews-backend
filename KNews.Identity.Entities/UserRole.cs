using Microsoft.AspNetCore.Identity;

namespace KNews.Identity.Entities
{
    /// <summary>
    /// Связь юзер - роль
    /// </summary>
    public class UserRole : IdentityUserRole<int> {

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
