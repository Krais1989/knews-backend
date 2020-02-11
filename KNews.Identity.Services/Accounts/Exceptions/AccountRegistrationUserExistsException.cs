using KNews.Identity.Services.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Accounts.Exceptions
{
    public class AccountRegistrationUserExistsException : BaseResponseException
    {
        public AccountRegistrationUserExistsException() 
            : base(GResponseCodes.AccountRegistrationUserExistsError, 400, "Такой пользователь уже существует.", null)
        {
        }
    }
}
