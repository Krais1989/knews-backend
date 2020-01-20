using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests.Communities.Validators
{
    public class CommunityDeleteValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void DeleteValidation(CommunityDeleteValidatorDto validatorDto, bool verify)
        {
            var validator = new CommunityDeleteValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommunityDeleteValidatorDto (
                    communityStatus: ECommunityStatus.Approved,
                    curUserStatus: EUserStatus.Approved,
                    curUserIsAdmin: true,
                    curUserIsOwner: false
                ), true);

                yield return new TestCaseData(new CommunityDeleteValidatorDto(
                    communityStatus: ECommunityStatus.Created,
                    curUserStatus: EUserStatus.Approved,
                    curUserIsAdmin: false,
                    curUserIsOwner: true
                ), true);
            }
        }
    }
}
