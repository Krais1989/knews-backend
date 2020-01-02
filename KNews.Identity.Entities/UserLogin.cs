using Microsoft.AspNetCore.Identity;

namespace KNews.Identity.Entities
{
    /// <summary>
    /// Связь юзер - вход
    /// </summary>
    public class UserLogin: IdentityUserLogin<int> {
        public virtual User User { get; set; }
    }
}
