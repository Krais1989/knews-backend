using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using KNews.Core.Services.Users.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KNews.Core.Services.UnitTests.Users.Validators
{
    public class CommunityUserInviteValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UserInviteValidation(UserInviteCommunityValidatorDto validatorDto, bool verify)
        {
            var validator = new UserInviteCommunityValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new UserInviteCommunityValidatorDto
                (
                    curUserStatus: EUserStatus.Approved,
                    targetUserStatus: EUserStatus.Approved,
                    communityStatus: ECommunityStatus.Approved,
                    communityInvitationPermission: ECommunityInvitationPermissions.Members,
                    curUserMembershipStatus: EUserMembershipStatus.Member,
                    tarUserMembershipStatus: EUserMembershipStatus.None,
                    tarUserInvitationStatus: EUserInvitationStatus.None
                ), true);
            }
        }
    }
}
