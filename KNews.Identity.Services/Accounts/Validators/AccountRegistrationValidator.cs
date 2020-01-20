using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Validators
{
    public class AccountRegistrationValidatorDto
    {
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }

        public AccountRegistrationValidatorDto(string email, string password, string phoneNumber)
        {
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
        }
    }

    public class AccountRegistrationValidator : AbstractValidator<AccountRegistrationValidatorDto>
    {
        public AccountRegistrationValidator()
        {
            RuleFor(dto => dto.Email).EmailAddress();
            RuleFor(dto => dto.PhoneNumber).Matches("[0-9]{10}").When(dto => !string.IsNullOrEmpty(dto.PhoneNumber));
        }
    }

}
