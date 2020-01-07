using FluentValidation;
using FluentValidation.Results;
using KNews.Core.Entities;
using System;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostDeleteValidatorDto
    {
        public DateTime PostCreateDate { get; set; }
        public bool CurUserIsAuthor { get; set; }

        public EUserStatus CurUserStatus { get; set; }
        public EXUserCommunityType? CurUserMembership { get; set; }

        public ECommunityPostDeletePermission CommPostDeletePermission { get; set; }
    }

    public class PostDeleteValidator : AbstractValidator<PostDeleteValidatorDto>
    {
        public PostDeleteValidator()
        {
            /* Или: Пользователь должен быть автором поста */
            /* И: прошло не более N-часов с момента создания поста */
            /* Или: Пользователь должен быть модератором сообщества, в случае если пост создан в этом сообществе */

            RuleFor(dto => dto.CurUserMembership)
                .NotNull()
                .Equal(EXUserCommunityType.Moderator)
                .When(dto => dto.CommPostDeletePermission == ECommunityPostDeletePermission.ModeratorOnly)
                    .WithMessage($"Action forbidden.");

            RuleFor(dto => dto.CurUserIsAuthor)
                .Equal(true)
                .When(dto => dto.CommPostDeletePermission == ECommunityPostDeletePermission.AuthorOnly)
                .WithMessage($"Action forbidden."); ;
        }
    }
}