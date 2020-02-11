using FluentValidation;
using KNews.Identity.Services.Authentications.Validators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KNews.Identity.Services.Services;
using Microsoft.AspNetCore.Identity;

namespace KNews.Identity.Services.Authentications.Handlers
{
    public class EmailPasswordSignInResponse
    {
        public string Login { get; set; }
        public string Token { get; set; }
    }

    public class EmailPasswordSignInRequest : IRequest<EmailPasswordSignInResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class EmailPasswordSignInRequestHandler : IRequestHandler<EmailPasswordSignInRequest,
        EmailPasswordSignInResponse>
    {
        private readonly ILogger<EmailPasswordSignInRequestHandler> _logger;
        private readonly IValidator<EmailPasswordSignInValidatorDto> _validator;
        private readonly IdentityUserManager _userMan;
        private readonly IJWTFactory _jwtFactory;

        public EmailPasswordSignInRequestHandler(ILogger<EmailPasswordSignInRequestHandler> logger,
            IValidator<EmailPasswordSignInValidatorDto> validator,
            IdentityUserManager userMan,
            IJWTFactory jwtFactory)
        {
            _logger = logger;
            _validator = validator;
            _userMan = userMan;
            _jwtFactory = jwtFactory;
        }

        public async Task<EmailPasswordSignInResponse> Handle(EmailPasswordSignInRequest request,
            CancellationToken cancellationToken)
        {
            var user = await _userMan.FindByNameAsync(request.Email);
            var result = await _userMan.CheckPasswordAsync(user, request.Password);

            if (!result)
                throw new Exception($"Invalid Email or Password");

            var tokenClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var response = new EmailPasswordSignInResponse()
            {
                Login = user.Email,
                Token = _jwtFactory.Generate(tokenClaims)
            };
            
            // var validatorDto = new SignInValidatorDto() { };
            // _validator.Validate(validatorDto);
            // return new EmailPasswordSignInResponse();
            
            return response;
        }
    }
}