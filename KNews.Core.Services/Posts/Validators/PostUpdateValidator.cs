using FluentValidation;
using KNews.Core.Entities;
using Microsoft.Extensions.Options;
using System;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostUpdateValidatorDto
    {
        public EUserMembershipStatus CurUserMembership { get; private set; }
        public EUserStatus CurUserStatus { get; private set; }
        public bool CurUserIsAuthor { get; private set; }
        public EPostStatus PostStatus { get; private set; }
        public DateTime PostCreateDate { get; private set; }
        public string NewTitle { get; private set; }
        public string NewContent { get; private set; }

        public PostUpdateValidatorDto(
            EUserMembershipStatus curUserMembershipStatus,
            EUserStatus curUserStatus,
            bool curUserIsAuthor,
            EPostStatus postStatus,
            DateTime postCreateDate,
            string newTitle,
            string newContent)
        {
            CurUserMembership = curUserMembershipStatus;
            CurUserStatus = curUserStatus;
            CurUserIsAuthor = curUserIsAuthor;
            PostStatus = postStatus;
            PostCreateDate = postCreateDate;
            NewTitle = newTitle;
            NewContent = newContent;
        }
    }

    public class PostUpdateValidator : AbstractValidator<PostUpdateValidatorDto>
    {
        private readonly IOptions<CoreDomainOptions> _options;

        public PostUpdateValidator(IOptions<CoreDomainOptions> options)
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
                .GreaterThan(DateTime.UtcNow - _options.Value.UpdateAvailablePeriod)
                .When(dto => dto.CurUserIsAuthor)
                .WithMessage($"Post update period has been expired.");

            RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved);
            
            RuleFor(dto => dto.CurUserIsAuthor)
                .Equal(true)
                .When(dto => dto.PostStatus == EPostStatus.Created);

            RuleFor(dto => dto.CurUserIsAuthor).Equal(true).When(dto => dto.PostStatus == EPostStatus.Approved);
            RuleFor(dto => dto.CurUserMembership).Equal(EUserMembershipStatus.Moderator).When(dto => dto.PostStatus == EPostStatus.Check);
            RuleFor(dto => dto.PostStatus)
                .NotEqual(EPostStatus.Deleted)
                    .WithMessage($"Post not found.")
                .NotEqual(EPostStatus.Forbiden)
                    .WithMessage($"Post not found.");
        }


    }
}