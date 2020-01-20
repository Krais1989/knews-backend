using KNews.Core.Entities;
using KNews.Core.Services.Posts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.UnitTests.Posts.Validators
{
    public class PostDeleteValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void GetShortValidation(PostDeleteValidatorDto validatorDto, bool verify)
        {
            var validator = new PostDeleteValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new PostDeleteValidatorDto
                (
                    postCreateDate: DateTime.UtcNow - TimeSpan.FromMinutes(30),
                    curUserIsAuthor: true,
                    curUserStatus: EUserStatus.Approved,
                    curUserMembership: EUserMembershipStatus.Member,
                    commPostDeletePermission: ECommunityPostDeletePermissions.Authors
                ), true);
            }
        }
    }
}
