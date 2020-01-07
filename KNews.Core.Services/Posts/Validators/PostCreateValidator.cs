using FluentValidation;
using KNews.Core.Entities;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostCreateValidatorDto
    {
        public ECommunityStatus CommunityStatus { get; set; }
        public ECommunityPostCreatePermission CommunityCreatePermission { get; set; }
        public EUserStatus AuthorStatus { get; set; }
        public EXUserCommunityType? CurrentUserMembership { get; set; }

        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        public PostCreateValidatorDto() { }

        public PostCreateValidatorDto(
            ECommunityStatus communityStatus,
            ECommunityPostCreatePermission communityCreatePermission,
            EUserStatus authorStatus,
            EXUserCommunityType? currentUserMembership,
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
                .NotNull()
                .NotEqual(EXUserCommunityType.None)
                .When(dto => dto.CommunityCreatePermission == ECommunityPostCreatePermission.MembersOnly);
            RuleFor(e => e.CurrentUserMembership)
                .NotNull()
                .Equal(EXUserCommunityType.Moderator)
                .When(dto => dto.CommunityCreatePermission == ECommunityPostCreatePermission.ModeratorOnly);
        }
    }
}