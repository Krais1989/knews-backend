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
    public class AccountPhoneConfirmationResponse
    {
    }
    public class AccountPhoneConfirmationRequest : IRequest<AccountPhoneConfirmationResponse>
    {
        public string Phone { get; set; }
        public string Code { get; set; }
    }
    public class AccountPhoneConfirmationRequestHandler : IRequestHandler<AccountPhoneConfirmationRequest, AccountPhoneConfirmationResponse>
    {
        private readonly ILogger<AccountPhoneConfirmationRequestHandler> _logger;
        private readonly IValidator<AccountPhoneConfirmationValidator> _validator;

        public async Task<AccountPhoneConfirmationResponse> Handle(AccountPhoneConfirmationRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new AccountPhoneConfirmationValidatorDto() { };
            _validator.Validate(validatorDto);
            return new AccountPhoneConfirmationResponse();
        }
    }
}
