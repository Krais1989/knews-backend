using FluentValidation;
using KNews.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Members.Validators
{
    public class MemberExpelCommunityValidatorDto
    {
        public EUserMembershipStatus CurUserMembershipStatus { get; private set; }
        public EUserMembershipStatus TarUserMembershipStatus { get; private set; }

        public MemberExpelCommunityValidatorDto(EUserMembershipStatus curUserMembershipStatus, EUserMembershipStatus tarUserMembershipStatus)
        {
            CurUserMembershipStatus = curUserMembershipStatus;
            TarUserMembershipStatus = tarUserMembershipStatus;
        }
    }

    public class MemberExpelCommunityValidator : AbstractValidator<MemberExpelCommunityValidatorDto>
    {
        public MemberExpelCommunityValidator()
        {
            RuleFor(dto => dto.CurUserMembershipStatus).Equal(EUserMembershipStatus.Moderator);
            RuleFor(dto => dto.TarUserMembershipStatus).Equal(EUserMembershipStatus.Member);
        }
    }

}
