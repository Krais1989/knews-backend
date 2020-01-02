using FluentValidation;
using KNews.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KNews.Core.Services.CommunityValidators
{
    public class CommunityUserLeaveValidatorDto
    {
        public CommunityUserLeaveValidatorDto(User user, Community community, IEnumerable<XCommunityUser> xCommunityUsers)
        {
            User = user;
            Community = community;
            XCommunityUsers = xCommunityUsers;
        }

        public User User { get; set; }
        public Community Community { get; set; }
        public IEnumerable<XCommunityUser> XCommunityUsers { get; set; }
    }

    public class CommunityUserLeaveValidator : AbstractValidator<CommunityUserLeaveValidatorDto>
    {
        public CommunityUserLeaveValidator()
        {
            RuleFor(e => e.User).NotNull();
            //RuleFor(e => e.User.Status).Equal(EUserStatus.Approved);

            RuleFor(e => e.Community).NotNull();
            //RuleFor(e => e.Community.Status).Must(e=>);

            RuleFor(e => e.XCommunityUsers).Must((dto, cu) => cu.Any(e => e.CommunityID == dto.Community.ID && e.UserID == dto.User.ID && e.Type != EXUserCommunityType.None));
        }
    }

}
