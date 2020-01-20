using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityUpdateValidatorDto
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Rules { get; private set; }

        public EUserStatus CurUserStatus { get; private set; }

        public ECommunityStatus CommunityStatus { get; private set; }

        public bool CurUserIsOwner { get; private set; }
        public bool CurUserIsAdmin { get; private set; }

        public CommunityUpdateValidatorDto(
            string title,
            string description,
            string rules,
            EUserStatus curUserStatus,
            ECommunityStatus communityStatus,
            bool curUserIsOwner,
            bool curUserIsAdmin)
        {
            Title = title;
            Description = description;
            Rules = rules;
            CurUserStatus = curUserStatus;
            CommunityStatus = communityStatus;
            CurUserIsOwner = curUserIsOwner;
            CurUserIsAdmin = curUserIsAdmin;
        }
    }

    public class CommunityUpdateValidator : AbstractValidator<CommunityUpdateValidatorDto>
    {
        public CommunityUpdateValidator()
        {
            RuleFor(e => e.Title).NotNull().MinimumLength(5);
            RuleFor(e => e.Description).NotNull().MinimumLength(5);
            RuleFor(e => e.Rules).NotNull().MinimumLength(5);

            RuleFor(e => e.CommunityStatus).NotEqual(ECommunityStatus.Deleted);

            RuleFor(e => e.CurUserStatus).Equal(EUserStatus.Approved);
            RuleFor(e => e.CurUserIsOwner).Equal(true).When(dto => dto.CommunityStatus == ECommunityStatus.Created);
            RuleFor(e => e.CurUserIsAdmin).Equal(true).When(dto => dto.CommunityStatus == ECommunityStatus.Approved);
        }
    }
}