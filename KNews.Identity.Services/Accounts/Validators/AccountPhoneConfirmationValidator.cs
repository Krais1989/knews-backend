﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Validators
{
    public class AccountPhoneConfirmationValidatorDto
    {
    }

    public class AccountPhoneConfirmationValidator : AbstractValidator<AccountPhoneConfirmationValidatorDto>
    {
        public AccountPhoneConfirmationValidator()
        {
        }
    }

}
