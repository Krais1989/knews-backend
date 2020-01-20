using KNews.Core.Services.Posts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using KNews.Core.Entities;

namespace KNews.Core.Services.UnitTests.Posts.Validators
{
    public class PostCreateValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void GetFullValidation(PostCreateValidatorDto validatorDto, bool verify)
        {
            var validator = new PostCreateValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new PostCreateValidatorDto
                (
                    communityStatus: ECommunityStatus.Approved,
                    communityCreatePermission: ECommunityPostCreatePermissions.Members,
                    authorStatus: EUserStatus.Approved,
                    currentUserMembership: EUserMembershipStatus.Member,
                    postTitle: "New Title",
                    postContent: "New Content"
                ), true);
            }
        }
    }
}
