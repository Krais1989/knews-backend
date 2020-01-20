using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityFindValidatorDto
    {
        public string Text { get; private set; }
        public EUserStatus? CurUserStatus { get; private set; }

        public CommunityFindValidatorDto(string text, EUserStatus? curUserStatus)
        {
            Text = text;
            CurUserStatus = curUserStatus;
        }
    }

    public class CommunityFindValidator : AbstractValidator<CommunityFindValidatorDto>
    {
        public CommunityFindValidator()
        {
        }
    }

}