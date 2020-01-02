using FluentValidation;
using KNews.Core.Services.CommunityRequests;

namespace KNews.Core.Services.CommunityValidators
{
    public class CommunityGetFullValidator : AbstractValidator<CommunityGetFullRequest>
    {
        public CommunityGetFullValidator()
        {
            RuleFor(e => e.IDs).NotNull().Must(e => e.Length > 0 && e.Length < 1000);
        }
    }
}
