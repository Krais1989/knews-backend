using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests.Communities.Validators
{
    public class CommunityGetFullValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void GetFullValidation(CommunityGetFullValidatorDto validatorDto, bool verify)
        {
            var validator = new CommunityGetFullValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommunityGetFullValidatorDto
                (
                    communityStatus: ECommunityStatus.Approved,
                    communityReadPermission: ECommunityReadPermissions.All,
                    curUserStatus: EUserStatus.Approved,
                    membershipType: EUserMembershipStatus.Member,
                    curUserIsAuthor: true
                ), true);
            }
        }
    }
}
