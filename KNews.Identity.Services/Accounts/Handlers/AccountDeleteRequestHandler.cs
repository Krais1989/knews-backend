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
    public class AccountDeleteResponse
    {
    }
    public class AccountDeleteRequest : IRequest<AccountDeleteResponse>
    {
    }
    public class AccountDeleteRequestHandler : IRequestHandler<AccountDeleteRequest, AccountDeleteResponse>
    {
        private readonly ILogger<AccountDeleteRequestHandler> _logger;
        private readonly IValidator<AccountDeleteValidator> _validator;

        public async Task<AccountDeleteResponse> Handle(AccountDeleteRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new AccountDeleteValidatorDto() { };
            _validator.Validate(validatorDto);
            return new AccountDeleteResponse();
        }
    }
}
