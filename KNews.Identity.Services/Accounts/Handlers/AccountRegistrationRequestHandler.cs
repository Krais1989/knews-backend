using FluentValidation;
using KNews.Identity.Entities;
using KNews.Identity.Services.Accounts.Exceptions;
using KNews.Identity.Services.Accounts.Validators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KNews.Identity.Services.Services;

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
        private readonly IValidator<AccountRegistrationValidatorDto> _validator;

        private readonly IdentityUserManager _userMan;
        private readonly IdentityRoleManager _roleMan;

        public AccountRegistrationRequestHandler(
            ILogger<AccountRegistrationRequestHandler> logger,
            IValidator<AccountRegistrationValidatorDto> validator,
            IdentityUserManager userMan,
            IdentityRoleManager roleMan)
        {
            _logger = logger;
            _validator = validator;
            _userMan = userMan;
            _roleMan = roleMan;
        }

        public async Task<AccountRegistrationResponse> Handle(AccountRegistrationRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new AccountRegistrationValidatorDto(
                email: request.Email,
                password: request.Password,
                phoneNumber: request.PhoneNumber
                );

            var result = _validator.Validate(validatorDto);
            if (!result.IsValid)
                throw new AccountRegistrationValidatorException(result.Errors.Select(e => e.ErrorMessage).ToArray());

            if (await _userMan.FindByEmailAsync(request.Email) != null)
                throw new AccountRegistrationUserExistsException();

            var user = new User()
            {
                Email = request.Email,
                UserName = request.Email,
                //UserClaims = new List<UserClaim>()
                //{
                //    new UserClaim(){ ClaimType = ClaimTypes.Role, ClaimValue = "Employee"  }
                //}
            };

            var userCreateRes = await _userMan.CreateAsync(user, request.Password);
            if (!userCreateRes.Succeeded)
                throw new AccountRegistrationHandlerException(string.Join("\n", userCreateRes.Errors.Select(e => e.Description)));

            return new AccountRegistrationResponse();
        }
    }
}
