using FluentValidation;
using KNews.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KNews.Core.Services.Users.Validators
{
    /// <summary>
    /// Структура для проверки отправки приглашения юзеру
    /// </summary>
    public class UserInviteCommunityValidatorDto
    {
        public EUserStatus CurUserStatus { get; private set; }
        public EUserStatus TargetUserStatus { get; private set; }
        public ECommunityStatus CommunityStatus { get; private set; }
        public ECommunityInvitationPermissions CommunityInvitationPermission { get; private set; }

        public EUserMembershipStatus CurUserMembershipStatus { get; private set; }
        public EUserMembershipStatus TarUserMembershipStatus { get; private set; }
        public EUserInvitationStatus TarUserInvitationStatus { get; private set; }

        public UserInviteCommunityValidatorDto(
            EUserStatus curUserStatus,
            EUserStatus targetUserStatus,
            ECommunityStatus communityStatus,
            ECommunityInvitationPermissions communityInvitationPermission,
            EUserMembershipStatus curUserMembershipStatus,
            EUserMembershipStatus tarUserMembershipStatus,
            EUserInvitationStatus tarUserInvitationStatus)
        {
            CurUserStatus = curUserStatus;
            TargetUserStatus = targetUserStatus;
            CommunityStatus = communityStatus;
            CommunityInvitationPermission = communityInvitationPermission;
            CurUserMembershipStatus = curUserMembershipStatus;
            TarUserMembershipStatus = tarUserMembershipStatus;
            TarUserInvitationStatus = tarUserInvitationStatus;
        }
    }

    /// <summary>
    /// Проверка возможности отправить приглашение в сообщество
    /// </summary>
    public class UserInviteCommunityValidator : AbstractValidator<UserInviteCommunityValidatorDto>
    {
        public UserInviteCommunityValidator()
        {
            RuleFor(e => e.CurUserStatus).Equal(EUserStatus.Approved);
            RuleFor(e => e.TargetUserStatus).Equal(EUserStatus.Approved);
            RuleFor(e => e.CommunityStatus).Equal(ECommunityStatus.Approved);
            RuleFor(e => e.CommunityInvitationPermission).NotEqual(ECommunityInvitationPermissions.NoOne);

            RuleFor(e => e.TarUserMembershipStatus).Equal(EUserMembershipStatus.None);
            RuleFor(e => e.TarUserInvitationStatus).Equal(EUserInvitationStatus.None);

            RuleFor(dto => dto.CurUserMembershipStatus).NotNull().NotEqual(EUserMembershipStatus.None)
                .When(dto => dto.CommunityInvitationPermission == ECommunityInvitationPermissions.Members);

            RuleFor(dto => dto.CurUserMembershipStatus).NotNull().Equal(EUserMembershipStatus.Moderator)
                .When(dto => dto.CommunityInvitationPermission == ECommunityInvitationPermissions.Moderators);
        }
    }
}
