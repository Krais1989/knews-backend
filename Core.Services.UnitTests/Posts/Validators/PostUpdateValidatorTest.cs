using KNews.Core.Entities;
using KNews.Core.Services.Posts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.UnitTests.Posts.Validators
{
    public class PostUpdateValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UpdateValidation(PostUpdateValidatorDto validatorDto, bool verify)
        {
            var validator = new PostUpdateValidator(TestTools.CoreOptions);
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new PostUpdateValidatorDto
                (
                    curUserMembershipStatus: EUserMembershipStatus.Member,
                    curUserStatus: EUserStatus.Approved,
                    curUserIsAuthor: true,
                    postStatus: EPostStatus.Approved,
                    postCreateDate: DateTime.UtcNow - TimeSpan.FromMinutes(10),
                    newTitle: "New Title",
                    newContent: "New Content"
                ), true);
            }
        }
    }
}
