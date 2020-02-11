using KNews.Identity.Services.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Exceptions
{
    public class AccountRegistrationValidatorException : BaseResponseException
    {
        public AccountRegistrationValidatorException(string[] errorData) : base(GResponseCodes.AccountRegistrationFormInvalidError, 400, $"Ошибка при регистрации", errorData)
        {
        }
    }
}
