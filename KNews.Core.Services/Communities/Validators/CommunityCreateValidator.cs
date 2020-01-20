using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityCreateValidatorDto
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Rules { get; private set; }
        public EUserStatus CurUserStatus { get; private set; }

        public CommunityCreateValidatorDto(string title, string description, string rules, EUserStatus curUserStatus)
        {
            Title = title;
            Description = description;
            Rules = rules;
            CurUserStatus = curUserStatus;
        }
    }

    public class CommunityCreateValidator : AbstractValidator<CommunityCreateValidatorDto>
    {
        public CommunityCreateValidator()
        {
            RuleFor(e => e.Title).NotNull().MinimumLength(5);
            RuleFor(e => e.Description).NotNull().MinimumLength(5);
            RuleFor(e => e.Rules).NotNull().MinimumLength(5);

            RuleFor(e => e.CurUserStatus).Equal(EUserStatus.Approved);
        }
    }
}