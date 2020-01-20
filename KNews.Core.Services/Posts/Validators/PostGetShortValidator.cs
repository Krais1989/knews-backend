using FluentValidation;
using KNews.Core.Entities;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostGetShortValidatorDto
    {
        public ECommunityReadPermissions CommunityReadPermissions { get; private set; }
        public EUserMembershipStatus CurUserMembershipStatus { get; private set; }
        public EPostStatus PostStatus { get; private set; }
        public bool GetByAuthor { get; private set; }


        public PostGetShortValidatorDto(
            ECommunityReadPermissions communityReadPermissions,
            EUserMembershipStatus curUserMembershipStatus,
            EPostStatus postStatus,
            bool getByAuthor)
        {
            CommunityReadPermissions = communityReadPermissions;
            CurUserMembershipStatus = curUserMembershipStatus;
            PostStatus = postStatus;
            GetByAuthor = getByAuthor;
        }
    }

    public class PostGetShortValidator : AbstractValidator<PostGetShortValidatorDto>
    {
        public PostGetShortValidator()
        {
            RuleFor(dto => dto.PostStatus).NotEqual(EPostStatus.Deleted).NotEqual(EPostStatus.Forbiden);

            RuleFor(dto => dto.PostStatus).Equal(EPostStatus.Approved)
                .When(dto => !dto.GetByAuthor);

            RuleFor(dto => dto.CurUserMembershipStatus).NotEqual(EUserMembershipStatus.None)
                .When(dto => dto.CommunityReadPermissions == ECommunityReadPermissions.Members);
        }
    }
}