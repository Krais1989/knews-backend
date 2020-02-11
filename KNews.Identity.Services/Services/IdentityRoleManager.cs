using System.Collections.Generic;
using KNews.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace KNews.Identity.Services.Services
{
    /// <summary>
    /// Менеджер юзеров
    /// </summary>
    public class IdentityRoleManager : RoleManager<Role>
    {
        public IdentityRoleManager(IRoleStore<Role> store, IEnumerable<IRoleValidator<Role>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<Role>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
