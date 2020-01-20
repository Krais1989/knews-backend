using FluentValidation;
using KNews.Identity.Services.Accounts.Validators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Identity.Services.Accounts.Handlers
{
    public class AccountPunishResponse
    {
    }
    public class AccountPunishRequest : IRequest<AccountPunishResponse>
    {
    }
    public class AccountPunishRequestHandler : IRequestHandler<AccountPunishRequest, AccountPunishResponse>
    {
        private readonly ILogger<AccountPunishRequestHandler> _logger;
        private readonly IValidator<AccountPunishValidator> _validator;

        public async Task<AccountPunishResponse> Handle(AccountPunishRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new AccountPunishValidatorDto() { };
            _validator.Validate(validatorDto);
            return new AccountPunishResponse();
        }
    }
}
