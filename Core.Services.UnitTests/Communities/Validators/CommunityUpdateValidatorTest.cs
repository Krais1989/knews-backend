using KNews.Core.Entities;
using KNews.Core.Services.Communities.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KNews.Core.Services.UnitTests.Communities.Validators
{
    public class CommunityUpdateValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UpdateValidation(CommunityUpdateValidatorDto validatorDto, bool verify)
        {
            var validator = new CommunityUpdateValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommunityUpdateValidatorDto
                (
                    title: "New Title",
                    description: "New Description",
                    rules: "New Rules",
                    curUserStatus: EUserStatus.Approved,
                    communityStatus: ECommunityStatus.Approved,
                    curUserIsOwner: false,
                    curUserIsAdmin: true
                ), true);

                yield return new TestCaseData(new CommunityUpdateValidatorDto
                (
                    title: "New Title",
                    description: "New Description",
                    rules: "New Rules",
                    curUserStatus: EUserStatus.Approved,
                    communityStatus: ECommunityStatus.Created,
                    curUserIsOwner: true,
                    curUserIsAdmin: false
                ), true);
            }
        }
    }
}
