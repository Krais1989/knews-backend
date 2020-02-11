using KNews.Identity.Services.Accounts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.UnitTests.Accounts.Validators
{
    public class AccountDeleteValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void DeleteValidation(AccountDeleteValidatorDto validatorDto, bool verify)
        {
            var validator = new AccountDeleteValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new AccountDeleteValidatorDto(
                ), true);
            }
        }
    }
}
