using FluentValidation;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityUpdateValidator : AbstractValidator<CommunityUpdateRequest>
    {
        public CommunityUpdateValidator()
        {
            RuleFor(e => e.ID).GreaterThan(0);
        }
    }
}