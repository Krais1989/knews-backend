using KNews.Core.Entities;
using KNews.Core.Services.Comments.Validators;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.UnitTests.Comments
{
    public class CommentCreateValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void CreateValidation(CommentCreateValidatorDto validatorDto, bool verify)
        {
            var validator = new CommentCreateValidator(TestTools.CoreOptions);
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommentCreateValidatorDto
                (
                    content: "New Content",
                    authorStatus: EUserStatus.Approved,
                    postStatus: EPostStatus.Approved,
                    parentCommentStatus: null,
                    commentCreatePermissions: EPostCommentCreatePermissions.Allowed
                ), true);
            }
        }
    }
}
