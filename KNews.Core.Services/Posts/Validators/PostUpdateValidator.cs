using FluentValidation;
using KNews.Core.Entities;
using System;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostUpdateValidatorDto
    {        
        public EXUserCommunityType? CurUserMembership { get; set; }

        public EUserStatus CurUserStatus { get; set; }
        public bool CurUserIsAuthor { get; set; }

        public EPostStatus PostStatus { get; set; }
        public DateTime PostCreateDate { get; set; }
        public string NewTitle { get; set; }
        public string NewContent { get; set; }
    }

    public class PostUpdateValidator : AbstractValidator<PostUpdateValidatorDto>
    {
        private readonly CoreDomainOptions _options;

        public PostUpdateValidator(CoreDomainOptions options)
        {
            _options = options;

            /*
                Пост можно редактировать:
                1) Редактирует автор поста. Пост в статусе Created. Прошло меньше часа с момента создания
                2) Редактирует модератор группы. Пост в статусе Check/Approved

                Пост нельзя редактировать:
                1) Пост в статусе Forbiden/Deleted
             */

            RuleFor(dto => dto.PostCreateDate)
                .GreaterThan(DateTime.UtcNow - _options.UpdateAvailablePeriod)
                .When(dto => dto.CurUserIsAuthor)
                .WithMessage($"Post update period has been expired.");

            RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved);
            
            RuleFor(dto => dto.CurUserIsAuthor)
                .Equal(true)
                .When(dto => dto.PostStatus == EPostStatus.Created);

            RuleFor(dto => dto.CurUserIsAuthor).Equal(true).When(dto => dto.PostStatus == EPostStatus.Approved);
            RuleFor(dto => dto.CurUserMembership).Equal(EXUserCommunityType.Moderator).When(dto => dto.PostStatus == EPostStatus.Check);
            RuleFor(dto => dto.PostStatus)
                .NotEqual(EPostStatus.Deleted)
                    .WithMessage($"Post not found.")
                .NotEqual(EPostStatus.Forbiden)
                    .WithMessage($"Post not found.");
        }


    }
}