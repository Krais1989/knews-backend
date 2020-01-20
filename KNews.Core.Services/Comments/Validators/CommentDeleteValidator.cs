using FluentValidation;
using KNews.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Comments.Validators
{
    public class CommentDeleteValidatorDto
    {
        public EUserStatus CurUserStatus { get; private set; }
        public ECommentStatus CommentStatus { get; private set; }
        public EPostCommentDeletePermissions PostCommentDeletePermissions { get; set; }

        public CommentDeleteValidatorDto(
            EUserStatus curUserStatus,
            ECommentStatus commentStatus,
            EPostCommentDeletePermissions postCommentDeletePermissions)
        {
            CurUserStatus = curUserStatus;
            CommentStatus = commentStatus;
            PostCommentDeletePermissions = postCommentDeletePermissions;
        }
    }

    public class CommentDeleteValidator : AbstractValidator<CommentDeleteValidatorDto>
    {
        public CommentDeleteValidator()
        {
            RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved);
            RuleFor(dto => dto.CommentStatus).NotEqual(ECommentStatus.Deleted);
            RuleFor(dto => dto.PostCommentDeletePermissions).Equal(EPostCommentDeletePermissions.Allowed);
        }
    }

}
