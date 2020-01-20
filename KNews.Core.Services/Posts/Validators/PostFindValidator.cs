using FluentValidation;
using KNews.Core.Entities;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostFindValidatorDto
    {
        public string Text { get; private set; }
        public PostFindValidatorDto(string text)
        {
            Text = text;
        }
    }

    public class PostFindValidator : AbstractValidator<PostFindValidatorDto>
    {
        public PostFindValidator()
        {
            RuleFor(dto => dto.Text.Length).GreaterThanOrEqualTo(3).When(dto => !string.IsNullOrEmpty(dto.Text));
        }
    }
}