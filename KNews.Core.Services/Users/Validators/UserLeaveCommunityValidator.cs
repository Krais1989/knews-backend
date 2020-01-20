using FluentValidation;
using KNews.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KNews.Core.Services.Users.Validators
{
    public class UserLeaveCommunityValidatorDto
    {
        public EUserMembershipStatus CurUserMembershipStatus { get; private set; }

        public UserLeaveCommunityValidatorDto(EUserMembershipStatus curUserMembershipStatus)
        {
            CurUserMembershipStatus = curUserMembershipStatus;
        }
    }

    public class UserLeaveCommunityValidator : AbstractValidator<UserLeaveCommunityValidatorDto>
    {
        public UserLeaveCommunityValidator()
        {
            RuleFor(dto => dto.CurUserMembershipStatus).NotEqual(EUserMembershipStatus.None).NotEqual(EUserMembershipStatus.Expelled);
        }
    }
}