using KNews.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KNews.Core.Persistence.EntityConfigurations
{
    public class CommunityEntityConfiguration : IEntityTypeConfiguration<Community>
    {
        public void Configure(EntityTypeBuilder<Community> builder)
        {
            builder.HasKey(e => e.ID);
            builder.HasMany(c => c.UserCommunities).WithOne(uc => uc.Community).HasForeignKey(uc => uc.CommunityID);
            builder.HasMany(c => c.PostCommunities).WithOne(pc => pc.Community).HasForeignKey(uc => uc.PostID);
        }
    }


    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.ID);
        }
    }



    public class XCommunityUserEntityConfiguration : IEntityTypeConfiguration<XCommunityUser>
    {
        public void Configure(EntityTypeBuilder<XCommunityUser> builder)
        {
            builder.HasKey(e => new { e.CommunityID, e.UserID });
        }
    }


    public class XCommunityPostEntityConfiguration : IEntityTypeConfiguration<XCommunityPost>
    {
        public void Configure(EntityTypeBuilder<XCommunityPost> builder)
        {
            builder.HasKey(e => new { e.CommunityID, e.PostID });
        }
    }


    public class UserInvitationEntityConfiguration : IEntityTypeConfiguration<UserInvitation>
    {
        public void Configure(EntityTypeBuilder<UserInvitation> builder)
        {
            builder.HasKey(e => e.ID);
        }
    }


}
