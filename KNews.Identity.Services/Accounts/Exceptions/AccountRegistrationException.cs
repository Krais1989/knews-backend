using KNews.Identity.Services.Shared.Exceptions;

namespace KNews.Identity.Services.Accounts.Exceptions
{
    public class AccountRegistrationHandlerException : BaseResponseException
    {
        public AccountRegistrationHandlerException(params string[] data)
            : base(GResponseCodes.AccountRegistrationHandlerError, 400, "Ошибка при создании нового пользователя.", data)
        {
        }
    }
}
