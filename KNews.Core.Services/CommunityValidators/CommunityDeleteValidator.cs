using FluentValidation;
using KNews.Core.Services.CommunityRequests;

namespace KNews.Core.Services.CommunityValidators
{
    public class CommunityDeleteValidator : AbstractValidator<CommunityDeleteRequest>
    {
        public CommunityDeleteValidator()
        {
        }
    }

}
