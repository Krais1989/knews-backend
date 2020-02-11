using KNews.Identity.Services.Accounts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.UnitTests.Accounts.Validators
{
    public class AccountPunishValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void PunishValidation(AccountPunishValidatorDto validatorDto, bool verify)
        {
            var validator = new AccountPunishValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new AccountPunishValidatorDto(
                ), true);
            }
        }
    }
}
