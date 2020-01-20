using KNews.Core.Entities;
using KNews.Core.Services.Comments.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.UnitTests.Comments
{
    public class CommentGetForPostValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void CreateValidation(CommentGetForPostValidatorDto validatorDto, bool verify)
        {
            var validator = new CommentGetForPostValidator(TestTools.CoreOptions);
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommentGetForPostValidatorDto
                (
                    take: 10,
                    curUserStatus: EUserStatus.Approved,
                    postCommentReadPermissions: EPostCommentReadPermissions.Allowed,
                    isCurUserAuthenticated: true,
                    isCurUserCommunityMember: true
                ), true);
            }
        }
    }
}
