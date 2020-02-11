using FluentValidation;
using KNews.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KNews.Core.Services.Members.Validators
{
    public class MemberLeaveCommunityValidatorDto
    {
        public EUserMembershipStatus CurUserMembershipStatus { get; private set; }

        public MemberLeaveCommunityValidatorDto(EUserMembershipStatus curUserMembershipStatus)
        {
            CurUserMembershipStatus = curUserMembershipStatus;
        }
    }

    public class MemberLeaveCommunityValidator : AbstractValidator<MemberLeaveCommunityValidatorDto>
    {
        public MemberLeaveCommunityValidator()
        {
            RuleFor(dto => dto.CurUserMembershipStatus).NotEqual(EUserMembershipStatus.None).NotEqual(EUserMembershipStatus.Expelled);
        }
    }
}