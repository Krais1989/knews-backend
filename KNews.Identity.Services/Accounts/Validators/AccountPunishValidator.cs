using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Validators
{

    public class AccountPunishValidatorDto
    {
    }

    public class AccountPunishValidator : AbstractValidator<AccountPunishValidatorDto>
    {
        public AccountPunishValidator()
        {
        }
    }

}
