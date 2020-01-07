using FluentValidation;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityCreateValidator : AbstractValidator<CommunityCreateRequest>
    {
        public CommunityCreateValidator()
        {
            RuleFor(e => e.Title).NotNull().MinimumLength(5);
            RuleFor(e => e.OwnerID).GreaterThan(0);
        }
    }
}