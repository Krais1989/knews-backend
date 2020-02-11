using KNews.Identity.Services.Accounts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.UnitTests.Accounts.Validators
{
    public class AccountEmailConfirmationValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void EmailConfirmationValidation(AccountEmailConfirmationValidatorDto validatorDto, bool verify)
        {
            var validator = new AccountEmailConfirmationValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new AccountEmailConfirmationValidatorDto(
                ), true);
            }
        }
    }
}
