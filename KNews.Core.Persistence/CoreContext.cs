using System;
using KNews.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace KNews.Core.Persistence
{
    public class CoreContext : DbContext
    {
        public DbSet<Community> Communities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserInvitation> UserInvitations { get; set; }

        public DbSet<XCommunityUser> XCommunityUsers { get; set; }
        public DbSet<XCommunityPost> XCommunityPosts { get; set; }

        public CoreContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoreContext).Assembly);
        }

    }
}
