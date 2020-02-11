using KNews.Core.Entities;
using KNews.Core.Services.Members.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace KNews.Core.Services.UnitTests.Members.Validators
{
    public class MemberLeaveValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UserLeaveValidation(MemberLeaveCommunityValidatorDto validatorDto, bool verify)
        {
            var validator = new MemberLeaveCommunityValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new MemberLeaveCommunityValidatorDto
                (
                    EUserMembershipStatus.Member
                ), true);
                yield return new TestCaseData(new MemberLeaveCommunityValidatorDto
                (
                    EUserMembershipStatus.None
                ), false);
            }
        }
    }
}
