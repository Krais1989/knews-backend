using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace KNews.Identity.Entities
{
    /// <summary>
    /// Базовый юзер
    /// </summary>
    public class User : IdentityUser<int>
    {
        public ICollection<UserClaim> UserClaims { get; set; }
        public ICollection<UserLogin> UserLogins { get; set; }
        public ICollection<UserToken> UserTokens { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }

}
