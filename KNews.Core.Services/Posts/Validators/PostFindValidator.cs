using FluentValidation;
using KNews.Core.Entities;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostFindValidatorDto
    {
        public string Text { get; set; }
        public EUserStatus? CurUserStatus { get; set; }

        public bool IsAnonymous { get; set; }
    }

    public class PostFindValidator : AbstractValidator<PostFindValidatorDto>
    {
        public PostFindValidator()
        {
            RuleFor(dto => dto.Text.Length).GreaterThanOrEqualTo(3);
            //RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved);
        }
    }
}