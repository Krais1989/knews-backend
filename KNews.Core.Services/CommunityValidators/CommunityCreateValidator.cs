using FluentValidation;
using KNews.Core.Services.CommunityRequests;

namespace KNews.Core.Services.CommunityValidators
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
