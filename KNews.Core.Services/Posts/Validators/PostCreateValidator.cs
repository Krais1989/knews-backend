using FluentValidation;
using KNews.Core.Entities;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostCreateValidatorDto
    {
        public ECommunityStatus CommunityStatus { get; private set; }
        public ECommunityPostCreatePermissions CommunityCreatePermission { get; private set; }
        public EUserStatus AuthorStatus { get; private set; }
        public EUserMembershipStatus CurrentUserMembership { get; private set; }

        public string PostTitle { get; private set; }
        public string PostContent { get; private set; }

        public PostCreateValidatorDto(
            ECommunityStatus communityStatus,
            ECommunityPostCreatePermissions communityCreatePermission,
            EUserStatus authorStatus,
            EUserMembershipStatus currentUserMembership,
            string postTitle,
            string postContent)
        {
            CommunityStatus = communityStatus;
            CommunityCreatePermission = communityCreatePermission;
            AuthorStatus = authorStatus;
            CurrentUserMembership = currentUserMembership;
            PostTitle = postTitle;
            PostContent = postContent;
        }
    }

    public class PostCreateValidator : AbstractValidator<PostCreateValidatorDto>
    {
        public PostCreateValidator()
        {
            RuleFor(dto => dto.PostTitle).NotEmpty();
            RuleFor(dto => dto.PostTitle.Length).GreaterThan(1);
            RuleFor(dto => dto.PostContent).NotEmpty();
            
            RuleFor(e => e.AuthorStatus).Equal(EUserStatus.Approved);
            RuleFor(e => e.CommunityStatus).Equal(ECommunityStatus.Approved);
            RuleFor(e => e.CurrentUserMembership)
                .NotEqual(EUserMembershipStatus.None)
                .When(dto => dto.CommunityCreatePermission == ECommunityPostCreatePermissions.Members);
            RuleFor(e => e.CurrentUserMembership)
                .Equal(EUserMembershipStatus.Moderator)
                .When(dto => dto.CommunityCreatePermission == ECommunityPostCreatePermissions.Moderators);
        }
    }
}