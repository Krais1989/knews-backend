using FluentValidation;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityFindValidator : AbstractValidator<CommunityFindRequest>
    {
        public CommunityFindValidator()
        {
        }
    }
}