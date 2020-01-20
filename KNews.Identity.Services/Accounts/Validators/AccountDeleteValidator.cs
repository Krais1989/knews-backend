using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Validators
{
    public class AccountDeleteValidatorDto
    {
    }

    public class AccountDeleteValidator : AbstractValidator<AccountDeleteValidatorDto>
    {
        public AccountDeleteValidator()
        {
        }
    }

}
