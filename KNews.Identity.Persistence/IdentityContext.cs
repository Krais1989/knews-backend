using KNews.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;

namespace KNews.Identity.Persistence
{
    /// <summary>
    /// Контекст БД для обслуживания аккаунтов
    /// </summary>
    public class IdentityContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Настройка identity-пользователя */
            builder.Entity<User>(e =>
            {
                e.ToTable("AspNetUsers");
                e.HasKey(u => u.Id);
                e.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
                e.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

                e.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                e.HasMany(u => u.UserClaims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
                e.HasMany(u => u.UserLogins).WithOne(ul => ul.User).HasForeignKey(ul => ul.UserId);
                e.HasMany(u => u.UserTokens).WithOne(ut => ut.User).HasForeignKey(ut => ut.UserId);
                e.HasMany(u => u.UserRoles).WithOne(ur => ur.User).HasForeignKey(ur => ur.UserId);
            });

            /* Настройка identity-роли */
            builder.Entity<Role>(e =>
            {
                e.ToTable("AspNetRoles");
                e.HasKey(r => r.Id);
                e.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

                e.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                e.HasMany(r => r.UserRoles).WithOne(ur => ur.Role).HasForeignKey(ur => ur.RoleId);
                e.HasMany(r => r.RoleClaims).WithOne(rc => rc.Role).HasForeignKey(rc => rc.RoleId);
            });

            /* Настройка пользователь-утверждение */
            builder.Entity<UserClaim>(e =>
            {
                e.ToTable("AspNetUserClaims");
                e.HasKey(uc => new { uc.Id, uc.UserId });
            });

            /* Настройка пользователь-вход */
            builder.Entity<UserLogin>(e =>
            {
                e.ToTable("AspNetUserLogins");
                e.HasKey(x => new { x.LoginProvider, x.ProviderKey });
            });

            /* Настройка пользователь-токен */
            builder.Entity<UserToken>(e =>
            {
                e.ToTable("AspNetUserTokens");
                e.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
            });

            /* Настройка роль-утверждение */
            builder.Entity<RoleClaim>(e =>
            {
                e.ToTable("AspNetRoleClaims");
                e.HasKey(rc => rc.Id);
            });

            /* Настройка пользователь-роль */
            builder.Entity<UserRole>(e =>
            {
                e.ToTable("AspNetUserRoles");
                e.HasKey(ur => new { ur.UserId, ur.RoleId });
            });
        }
    }
}
