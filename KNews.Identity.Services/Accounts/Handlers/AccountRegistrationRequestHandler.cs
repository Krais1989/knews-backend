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
    public class AccountRegistrationResponse
    {
    }
    public class AccountRegistrationRequest : IRequest<AccountRegistrationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AccountRegistrationRequestHandler : IRequestHandler<AccountRegistrationRequest, AccountRegistrationResponse>
    {
        private readonly ILogger<AccountRegistrationRequestHandler> _logger;
        private readonly IValidator<AccountRegistrationValidator> _validator;

        public async Task<AccountRegistrationResponse> Handle(AccountRegistrationRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new AccountRegistrationValidatorDto(
                email: request.Email,
                password: request.Password,
                phoneNumber: request.PhoneNumber
                );
            _validator.Validate(validatorDto);

            return new AccountRegistrationResponse();
        }
    }
}
