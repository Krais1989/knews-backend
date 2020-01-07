using FluentValidation;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityDeleteValidator : AbstractValidator<CommunityDeleteRequest>
    {
        public CommunityDeleteValidator()
        {
        }
    }
}