using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Validators
{

    public class AccountEmailConfirmationValidatorDto
    {
    }

    public class AccountEmailConfirmationValidator : AbstractValidator<AccountEmailConfirmationValidatorDto>
    {
        public AccountEmailConfirmationValidator()
        {
        }
    }

}
