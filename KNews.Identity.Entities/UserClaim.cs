using Microsoft.AspNetCore.Identity;

namespace KNews.Identity.Entities
{
    /// <summary>
    /// Связь юзер - утверждение
    /// </summary>
    public class UserClaim : IdentityUserClaim<int> {
        //public virtual User User { get; set; }
    }
}
