using KNews.Core.Entities;
using KNews.Core.Services.Comments.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.UnitTests.Comments
{
    public class CommentUpdateValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void UpdateValidation(CommentUpdateValidatorDto validatorDto, bool verify)
        {
            var validator = new CommentUpdateValidator(TestTools.CoreOptions);
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommentUpdateValidatorDto
                (
                    commentCreateDate: DateTime.UtcNow - TimeSpan.FromMinutes(10),
                    newContent: "New Content",
                    authorStatus: EUserStatus.Approved,
                    postStatus: EPostStatus.Approved,
                    parentCommentStatus: ECommentStatus.Approved,
                    postCommentUpdatePermissions: EPostCommentUpdatePermissions.Allowed
                ), true);
            }
        }
    }
}
