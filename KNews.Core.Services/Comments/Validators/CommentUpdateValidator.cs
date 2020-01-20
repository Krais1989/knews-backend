using FluentValidation;
using KNews.Core.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Comments.Validators
{
    public class CommentUpdateValidatorDto
    {
        public DateTime CommentCreateDate { get; private set; }
        public string NewContent { get; private set; }
        public EUserStatus AuthorStatus { get; private set; }
        public EPostStatus PostStatus { get; private set; }
        public ECommentStatus? ParentCommentStatus { get; private set; }
        public EPostCommentUpdatePermissions PostCommentUpdatePermissions { get; private set; }

        public CommentUpdateValidatorDto(
            DateTime commentCreateDate,
            string newContent,
            EUserStatus authorStatus,
            EPostStatus postStatus,
            ECommentStatus? parentCommentStatus,
            EPostCommentUpdatePermissions postCommentUpdatePermissions)
        {
            CommentCreateDate = commentCreateDate;
            NewContent = newContent;
            AuthorStatus = authorStatus;
            PostStatus = postStatus;
            ParentCommentStatus = parentCommentStatus;
            PostCommentUpdatePermissions = postCommentUpdatePermissions;
        }
    }

    public class CommentUpdateValidator : AbstractValidator<CommentUpdateValidatorDto>
    {
        private readonly IOptions<CoreDomainOptions> _options;
        public CommentUpdateValidator(IOptions<CoreDomainOptions> options)
        {
            _options = options;
            RuleFor(dto => dto.NewContent.Length).GreaterThanOrEqualTo(_options.Value.MinCommentarySize);
            RuleFor(dto => dto.NewContent.Length).LessThanOrEqualTo(_options.Value.MaxCommentarySize);
            RuleFor(dto => dto.AuthorStatus).Equal(EUserStatus.Approved);
            RuleFor(dto => dto.PostStatus).Equal(EPostStatus.Approved);

            RuleFor(dto => dto.PostCommentUpdatePermissions).NotEqual(EPostCommentUpdatePermissions.NotAllowed);

            RuleFor(dto => dto.CommentCreateDate).GreaterThan(DateTime.UtcNow - _options.Value.UpdateAvailablePeriod);
        }


    }

}
