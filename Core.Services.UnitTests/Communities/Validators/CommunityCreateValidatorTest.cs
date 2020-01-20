using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests.Communities.Validators
{
    [TestFixture]
    public class CommunityCreateValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void CreateValidation(CommunityCreateValidatorDto validatorDto, bool verify)
        {
            var validator = new CommunityCreateValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommunityCreateValidatorDto
                (
                    title: "Title 1",
                    description: "Description 1",
                    rules: "Rules section",
                    curUserStatus: EUserStatus.Approved
                ), true);
            }
        }
    }
}
