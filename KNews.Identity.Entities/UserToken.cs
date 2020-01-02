using Microsoft.AspNetCore.Identity;

namespace KNews.Identity.Entities
{
    /// <summary>
    /// Связь юзер - токен
    /// </summary>
    public class UserToken : IdentityUserToken<int> {
        public virtual User User { get; set; }
    }
}
