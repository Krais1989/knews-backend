using FluentValidation;
using KNews.Core.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Comments.Validators
{
    public class CommentCreateValidatorDto
    {
        public string Content { get; private set; }
        public EUserStatus AuthorStatus { get; private set; }
        public EPostStatus PostStatus { get; private set; }
        public ECommentStatus? ParentCommentStatus { get; private set; }
        public EPostCommentCreatePermissions CommentCreatePermissions { get; private set; }

        public CommentCreateValidatorDto(
            string content,
            EUserStatus authorStatus,
            EPostStatus postStatus,
            ECommentStatus? parentCommentStatus,
            EPostCommentCreatePermissions commentCreatePermissions)
        {
            Content = content;
            AuthorStatus = authorStatus;
            PostStatus = postStatus;
            ParentCommentStatus = parentCommentStatus;
            CommentCreatePermissions = commentCreatePermissions;
        }
    }

    public class CommentCreateValidator : AbstractValidator<CommentCreateValidatorDto>
    {
        private readonly IOptions<CoreDomainOptions> _options;

        public CommentCreateValidator(IOptions<CoreDomainOptions> options)
        {
            _options = options;

            RuleFor(dto => dto.Content.Length).GreaterThanOrEqualTo(_options.Value.MinCommentarySize);
            RuleFor(dto => dto.Content.Length).LessThanOrEqualTo(_options.Value.MaxCommentarySize);
            RuleFor(dto => dto.AuthorStatus).Equal(EUserStatus.Approved);
            RuleFor(dto => dto.PostStatus).Equal(EPostStatus.Approved);
            RuleFor(dto => dto.ParentCommentStatus).NotEqual(ECommentStatus.Posted).When(dto => dto.ParentCommentStatus != null);

            RuleFor(dto => dto.CommentCreatePermissions).Equal(EPostCommentCreatePermissions.Allowed);
        }
    }

}
