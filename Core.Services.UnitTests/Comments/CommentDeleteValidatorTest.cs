using KNews.Core.Entities;
using KNews.Core.Services.Comments.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.UnitTests.Comments
{
    public class CommentDeleteValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void DeleteValidation(CommentDeleteValidatorDto validatorDto, bool verify)
        {
            var validator = new CommentDeleteValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new CommentDeleteValidatorDto(
                    curUserStatus: EUserStatus.Approved,
                    commentStatus: ECommentStatus.Approved,
                    postCommentDeletePermissions: EPostCommentDeletePermissions.Allowed
                ), true);
            }
        }
    }
}
