using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Validators
{
    public class AccountChangePasswordValidatorDto
    {
    }

    public class AccountChangePasswordValidator : AbstractValidator<AccountChangePasswordValidatorDto>
    {
        public AccountChangePasswordValidator()
        {
        }
    }

}
