using FluentValidation;
using KNews.Core.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Comments.Validators
{
    public class CommentGetForPostValidatorDto
    {
        public int Take { get; private set; }
        public EUserStatus CurUserStatus { get; private set; }
        public EPostCommentReadPermissions PostCommentReadPermissions { get; private set; }
        public bool IsCurUserAuthenticated { get; private set; }
        public bool IsCurUserCommunityMember { get; private set; }

        public CommentGetForPostValidatorDto(
            int take,
            EUserStatus curUserStatus,
            EPostCommentReadPermissions postCommentReadPermissions,
            bool isCurUserAuthenticated,
            bool isCurUserCommunityMember)
        {
            Take = take;
            CurUserStatus = curUserStatus;
            PostCommentReadPermissions = postCommentReadPermissions;
            IsCurUserAuthenticated = isCurUserAuthenticated;
            IsCurUserCommunityMember = isCurUserCommunityMember;
        }
    }

    public class CommentGetForPostValidator : AbstractValidator<CommentGetForPostValidatorDto>
    {
        private readonly IOptions<CoreDomainOptions> _options;

        public CommentGetForPostValidator(IOptions<CoreDomainOptions> options)
        {
            _options = options;
        }

        public CommentGetForPostValidator()
        {
            RuleFor(dto => dto.Take).LessThanOrEqualTo(_options.Value.MaxCommentaryTake);
            RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved).When(dto => dto.IsCurUserAuthenticated);
            RuleFor(dto => dto.PostCommentReadPermissions).NotEqual(EPostCommentReadPermissions.NotAllowed);
            RuleFor(dto => dto.IsCurUserAuthenticated).Equal(true)
                .When(dto => dto.PostCommentReadPermissions == EPostCommentReadPermissions.AllowedAuthenticated);
            RuleFor(dto => dto.IsCurUserCommunityMember).Equal(true)
                .When(dto => dto.PostCommentReadPermissions == EPostCommentReadPermissions.AllowedCommunityMembers);
        }


    }

}
