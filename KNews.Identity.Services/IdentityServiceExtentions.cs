using KNews.Identity.Entities;
using KNews.Identity.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddKNewsIdentity(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }

        public static IApplicationBuilder AddKNewsIdentity(this IApplicationBuilder builder)
        {
            return builder;
        }
    }
}
