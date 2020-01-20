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
    public class AccountEmailConfirmationResponse
    {
    }
    public class AccountEmailConfirmationRequest : IRequest<AccountEmailConfirmationResponse>
    {
    }
    public class AccountEmailConfirmationRequestHandler : IRequestHandler<AccountEmailConfirmationRequest, AccountEmailConfirmationResponse>
    {
        private readonly ILogger<AccountEmailConfirmationRequestHandler> _logger;
        private readonly IValidator<AccountEmailConfirmationValidator> _validator;

        public async Task<AccountEmailConfirmationResponse> Handle(AccountEmailConfirmationRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new AccountEmailConfirmationValidatorDto() { };
            _validator.Validate(validatorDto);
            return new AccountEmailConfirmationResponse();
        }
    }
}
