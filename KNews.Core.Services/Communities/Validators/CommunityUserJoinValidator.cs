using FluentValidation;
using KNews.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityUserJoinValidatorDto
    {
        public User User { get; set; }
        public Community Community { get; set; }

        public IEnumerable<XCommunityUser> XCommunityUsers { get; set; }
        public IEnumerable<UserInvitation> UserInvitations { get; set; }
    }

    public class CommunityUserJoinValidator : AbstractValidator<CommunityUserJoinValidatorDto>
    {
        public CommunityUserJoinValidator()
        {
            RuleFor(e => e.User).NotNull();
            RuleFor(e => e.User.Status).Equal(EUserStatus.Approved);

            RuleFor(e => e.Community).NotNull();
            RuleFor(e => e.Community.Status).Equal(ECommunityStatus.Approved);

            RuleFor(e => e.XCommunityUsers).Must((dto, cu) => !cu.Any(e => e.CommunityID == dto.Community.ID && e.UserID == dto.User.ID && e.Type != EXUserCommunityType.None));
            RuleFor(e => e.UserInvitations).Must((dto, ui) => !ui.Any(e => e.InvitedUserID == dto.User.ID && e.CommunityID == dto.Community.ID));
        }
    }
}