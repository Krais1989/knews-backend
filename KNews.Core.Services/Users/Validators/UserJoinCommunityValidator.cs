using FluentValidation;
using KNews.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KNews.Core.Services.Users.Validators
{
    public class UserJoinCommunityValidatorDto
    {
        public EUserStatus CurUserStatus { get; private set; }
        public ECommunityStatus CommunityStatus { get; private set; }
        public ECommunityJoinPermissions CommunityJoinPermissions { get; private set; }
        public EUserMembershipStatus CurUserMembershipStatus { get; private set; }
        public EUserInvitationStatus CurUserInvitationStatus { get; private set; }

        public UserJoinCommunityValidatorDto(
            EUserStatus curUserStatus,
            ECommunityStatus communityStatus,
            ECommunityJoinPermissions communityJoinPermissions,
            EUserMembershipStatus curUserMembershipStatus,
            EUserInvitationStatus curUserInvitationStatus)
        {
            CurUserStatus = curUserStatus;
            CommunityStatus = communityStatus;
            CommunityJoinPermissions = communityJoinPermissions;
            CurUserMembershipStatus = curUserMembershipStatus;
            CurUserInvitationStatus = curUserInvitationStatus;
        }

    }

    public class UserJoinCommunityValidator : AbstractValidator<UserJoinCommunityValidatorDto>
    {
        public UserJoinCommunityValidator()
        {
            RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved);
            RuleFor(dto => dto.CommunityStatus).Equal(ECommunityStatus.Approved);
            RuleFor(dto => dto.CurUserMembershipStatus).Equal(EUserMembershipStatus.None);
            
            RuleFor(dto => dto.CommunityJoinPermissions).NotEqual(ECommunityJoinPermissions.NotAllowed);
            RuleFor(dto => dto.CurUserInvitationStatus).Equal(EUserInvitationStatus.Recieved)
                .When(dto => dto.CommunityJoinPermissions == ECommunityJoinPermissions.ByInvitation);
        }
    }
}