using FluentValidation;
using KNews.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Users.Validators
{
    public class UserExpelCommunityValidatorDto
    {
        public EUserMembershipStatus CurUserMembershipStatus { get; private set; }
        public EUserMembershipStatus TarUserMembershipStatus { get; private set; }

        public UserExpelCommunityValidatorDto(EUserMembershipStatus curUserMembershipStatus, EUserMembershipStatus tarUserMembershipStatus)
        {
            CurUserMembershipStatus = curUserMembershipStatus;
            TarUserMembershipStatus = tarUserMembershipStatus;
        }
    }

    public class UserExpelCommunityValidator : AbstractValidator<UserExpelCommunityValidatorDto>
    {
        public UserExpelCommunityValidator()
        {
            RuleFor(dto => dto.CurUserMembershipStatus).Equal(EUserMembershipStatus.Moderator);
            RuleFor(dto => dto.TarUserMembershipStatus).Equal(EUserMembershipStatus.Member);
        }
    }

}
