using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using KNews.Core.Services.Users.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KNews.Core.Services.UnitTests.Users.Validators
{
    public class CommunityUserJoinValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UserJoinValidation(UserJoinCommunityValidatorDto validatorDto, bool verify)
        {
            var validator = new UserJoinCommunityValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new UserJoinCommunityValidatorDto
                (
                    curUserStatus: EUserStatus.Approved,
                    communityStatus: ECommunityStatus.Approved,
                    communityJoinPermissions: ECommunityJoinPermissions.Allowed,
                    curUserMembershipStatus: EUserMembershipStatus.None,
                    curUserInvitationStatus: EUserInvitationStatus.None
                ), true);
            }
        }
    }
}
