using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests.Communities.Validators
{
    public class CommunityFindValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void FindValidation(CommunityFindValidatorDto verifyDto, bool verify)
        {
            var validator = new CommunityFindValidator();
            var result = validator.Validate(verifyDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommunityFindValidatorDto(
                    text: "Anything",
                    curUserStatus: EUserStatus.Approved
                ), true);
                yield return new TestCaseData(new CommunityFindValidatorDto(
                    text: "",
                    curUserStatus: EUserStatus.Approved
                ), true);
            }
        }
    }

}
