using FluentValidation;
using FluentValidation.Results;
using KNews.Core.Entities;
using System;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostDeleteValidatorDto
    {
        public DateTime PostCreateDate { get; private set; }
        public bool CurUserIsAuthor { get; private set; }
        public EUserStatus CurUserStatus { get; private set; }
        public EUserMembershipStatus CurUserMembership { get; private set; }
        public ECommunityPostDeletePermissions CommPostDeletePermission { get; private set; }

        public PostDeleteValidatorDto(
            DateTime postCreateDate,
            bool curUserIsAuthor,
            EUserStatus curUserStatus,
            EUserMembershipStatus curUserMembership,
            ECommunityPostDeletePermissions commPostDeletePermission)
        {
            PostCreateDate = postCreateDate;
            CurUserIsAuthor = curUserIsAuthor;
            CurUserStatus = curUserStatus;
            CurUserMembership = curUserMembership;
            CommPostDeletePermission = commPostDeletePermission;
        }
    }

    public class PostDeleteValidator : AbstractValidator<PostDeleteValidatorDto>
    {
        public PostDeleteValidator()
        {
            /* Или: Пользователь должен быть автором поста */
            /* И: прошло не более N-часов с момента создания поста */
            /* Или: Пользователь должен быть модератором сообщества, в случае если пост создан в этом сообществе */

            RuleFor(dto => dto.CurUserMembership)
                .Equal(EUserMembershipStatus.Moderator)
                .When(dto => dto.CommPostDeletePermission == ECommunityPostDeletePermissions.Moderators)
                .WithMessage($"Action forbidden.");

            RuleFor(dto => dto.CurUserIsAuthor)
                .Equal(true)
                .Unless(dto => dto.CurUserMembership == EUserMembershipStatus.Moderator)
                .When(dto => dto.CommPostDeletePermission == ECommunityPostDeletePermissions.Authors)
                .WithMessage($"Action forbidden."); ;
        }
    }
}