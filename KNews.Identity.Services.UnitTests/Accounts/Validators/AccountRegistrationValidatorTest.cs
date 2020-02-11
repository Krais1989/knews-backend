using KNews.Identity.Services.Accounts.Validators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.UnitTests.Accounts.Validators
{
    public class AccountRegistrationValidatorTest
    {
        [Test, TestCaseSource(nameof(ValidationCases))]
        public void RegistrationValidation(AccountRegistrationValidatorDto validatorDto, bool verify)
        {
            var validator = new AccountRegistrationValidator();
            var result = validator.Validate(validatorDto);
            Assert.AreEqual(result.IsValid, verify);
        }

        static IEnumerable<TestCaseData> ValidationCases
        {
            get
            {
                yield return new TestCaseData(new AccountRegistrationValidatorDto(
                    email: "asd@asd.ru",
                    password: "pass1",
                    phoneNumber: "1234567890"
                ), true);
            }
        }
    }
}
