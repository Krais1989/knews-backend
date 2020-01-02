using FluentValidation;
using KNews.Core.Services.CommunityRequests;

namespace KNews.Core.Services.CommunityValidators
{
    public class CommunityFindValidator : AbstractValidator<CommunityFindRequest>
    {
        public CommunityFindValidator()
        {
        }
    }
}
