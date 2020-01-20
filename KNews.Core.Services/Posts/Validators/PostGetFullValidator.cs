using System;

using FluentValidation;
using KNews.Core.Entities;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostGetFullValidatorDto
    {
        public ECommunityReadPermissions CommunityReadPermissions { get; set; }
        public EUserMembershipStatus? MemberStatus { get; set; }
        public EPostStatus PostStatus { get; set; }
        public bool GetByAuthor { get; set; }
        
        public PostGetFullValidatorDto(
            ECommunityReadPermissions communityReadPermissions,
            EUserMembershipStatus curUserMembershipStatus,
            EPostStatus postStatus,
            bool getByAuthor)
        {
            CommunityReadPermissions = communityReadPermissions;
            MemberStatus = curUserMembershipStatus;
            PostStatus = postStatus;
            GetByAuthor = getByAuthor;
        }
    }

    public class PostGetFullValidator : AbstractValidator<PostGetFullValidatorDto>
    {
        public PostGetFullValidator()
        {
            RuleFor(dto => dto.PostStatus).NotEqual(EPostStatus.Deleted).NotEqual(EPostStatus.Forbiden);

            RuleFor(dto => dto.PostStatus).Equal(EPostStatus.Approved).When(dto => !dto.GetByAuthor);
            RuleFor(dto => dto.MemberStatus).NotEqual(EUserMembershipStatus.None)
                .When(dto => dto.CommunityReadPermissions == ECommunityReadPermissions.Members);
        }
    }
}