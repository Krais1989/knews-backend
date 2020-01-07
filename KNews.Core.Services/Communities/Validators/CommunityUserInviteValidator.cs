using FluentValidation;
using KNews.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KNews.Core.Services.Communities.Validators
{
    /// <summary>
    /// Структура для проверки отправки приглашения юзеру
    /// </summary>
    public class CommunityUserInviteValidatorDto
    {
        public CommunityUserInviteValidatorDto(
            User invitingUser,
            User invitedUser,
            Community community,
            IEnumerable<UserInvitation> existUserInvitations,
            XCommunityUser invitedXCommunityUser,
            XCommunityUser invitingXCommunityUser)
        {
            InvitingUser = invitingUser;
            InvitedUser = invitedUser;
            Community = community;
            ExistUserInvitations = existUserInvitations;
            InvitedXCommunityUser = invitedXCommunityUser;
            InvitingXCommunityUser = invitingXCommunityUser;
        }

        public User InvitingUser { get; set; }
        public User InvitedUser { get; set; }
        public Community Community { get; set; }
        public XCommunityUser InvitedXCommunityUser { get; set; }
        public XCommunityUser InvitingXCommunityUser { get; set; }
        public IEnumerable<UserInvitation> ExistUserInvitations { get; set; }
    }

    /// <summary>
    /// Проверка возможности отправить приглашение в сообщество
    /// </summary>
    public class CommunityUserInviteValidator : AbstractValidator<CommunityUserInviteValidatorDto>
    {
        public CommunityUserInviteValidator()
        {
            RuleFor(e => e.InvitedUser).NotNull();
            RuleFor(e => e.InvitedUser.Status).Equal(EUserStatus.Approved);

            RuleFor(e => e.InvitingUser).NotNull();
            RuleFor(e => e.InvitingUser.Status).Equal(EUserStatus.Approved);

            RuleFor(e => e.Community).NotNull();
            RuleFor(e => e.Community.Status).Equal(ECommunityStatus.Approved);
            RuleFor(e => e.Community.InvitationsAvailable).Equal(true);

            RuleFor(e => e.InvitedXCommunityUser).Must((dto, cu) => cu == null || cu.Type == EXUserCommunityType.None)
                .WithMessage((dto, cu) => { return $"Invited user {cu.UserID} already in community {cu.CommunityID}"; });

            RuleFor(e => e.InvitingXCommunityUser).Must((dto, cu) => cu != null && cu.Type == EXUserCommunityType.Moderator)
                .WithMessage((dto, cu) => { return $"Inviting user {dto.InvitingUser.ID} has no permissions to invite to community {dto.Community.ID}"; });

            RuleFor(e => e.ExistUserInvitations).Must((dto, ui) => !ui.Any(e => e.CommunityID == dto.Community.ID && e.Status != EUserInvitationStatus.Ignored))
                .WithMessage((dto, cu) => { return $"Invited user {dto.InvitedUser.ID} already have invitation to community {dto.Community.ID}"; }); ;
        }
    }
}