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
    public class AccountChangePasswordResponse
    {
    }
    public class AccountChangePasswordRequest : IRequest<AccountChangePasswordResponse>
    {
    }
    public class AccountChangePasswordRequestHandler : IRequestHandler<AccountChangePasswordRequest, AccountChangePasswordResponse>
    {
        private readonly ILogger<AccountChangePasswordRequestHandler> _logger;
        private readonly IValidator<AccountChangePasswordValidator> _validator;

        public async Task<AccountChangePasswordResponse> Handle(AccountChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new AccountChangePasswordValidatorDto() { };
            _validator.Validate(validatorDto);
            return new AccountChangePasswordResponse();
        }
    }
}
