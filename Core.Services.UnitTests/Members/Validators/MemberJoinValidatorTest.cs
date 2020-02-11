using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using KNews.Core.Services.Members.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KNews.Core.Services.UnitTests.Members.Validators
{
    public class MemberJoinValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UserJoinValidation(MemberJoinCommunityValidatorDto validatorDto, bool verify)
        {
            var validator = new MemberJoinCommunityValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new MemberJoinCommunityValidatorDto
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
