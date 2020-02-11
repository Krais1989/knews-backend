using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using KNews.Core.Services.Members.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KNews.Core.Services.UnitTests.Members.Validators
{
    public class MemberInviteValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UserInviteValidation(MemberInviteCommunityValidatorDto validatorDto, bool verify)
        {
            var validator = new MemberInviteCommunityValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new MemberInviteCommunityValidatorDto
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
