using FluentValidation;
using KNews.Core.Entities;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostGetFullValidatorDto
    {
        public ECommunityReadPermission CommunityReadPermissions { get; set; }
        public EXUserCommunityType? MemberStatus { get; set; }
        public EPostStatus PostStatus { get; set; }
        public bool GetByAuthor { get; set; }

        public PostGetFullValidatorDto() { }

        public PostGetFullValidatorDto(ECommunityReadPermission communityReadPermissions, EXUserCommunityType? memberStatus, EPostStatus postStatus, bool getByAuthor)
        {
            CommunityReadPermissions = communityReadPermissions;
            MemberStatus = memberStatus;
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
            RuleFor(dto => dto.MemberStatus)
                .Must(dto => dto != null && dto != EXUserCommunityType.None)
                .When(dto => dto.CommunityReadPermissions == ECommunityReadPermission.MembersOnly);
        }
    }
}