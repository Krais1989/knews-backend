using KNews.Core.Entities;
using KNews.Core.Services.Posts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.UnitTests.Posts.Validators
{
    public class PostGetShortValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void GetShortValidation(PostGetShortValidatorDto validatorDto, bool verify)
        {
            var validator = new PostGetShortValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new PostGetShortValidatorDto
                (
                    communityReadPermissions: ECommunityReadPermissions.All,
                    curUserMembershipStatus: EUserMembershipStatus.Member,
                    postStatus: EPostStatus.Approved,
                    getByAuthor: false
                ), true);
            }
        }
    }
}
