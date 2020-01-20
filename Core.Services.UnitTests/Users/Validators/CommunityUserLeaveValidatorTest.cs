using KNews.Core.Entities;
using KNews.Core.Services.Users.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KNews.Core.Services.UnitTests.Users.Validators
{
    public class CommunityUserLeaveValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UserLeaveValidation(UserLeaveCommunityValidatorDto validatorDto, bool verify)
        {
            var validator = new UserLeaveCommunityValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new UserLeaveCommunityValidatorDto
                (
                    EUserMembershipStatus.Member
                ), true);
                yield return new TestCaseData(new UserLeaveCommunityValidatorDto
                (
                    EUserMembershipStatus.None
                ), false);
            }
        }
    }
}
