﻿using KNews.Identity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KNews.Identity.Services.Services
{
    /// <summary>
    /// Менеджер входа
    /// </summary>
    public class IdentitySignInManager : SignInManager<User>
    {
        public IdentitySignInManager(
            UserManager<User> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
    }
}
