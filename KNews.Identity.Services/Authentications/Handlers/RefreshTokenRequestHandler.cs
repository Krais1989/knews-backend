using FluentValidation;
using KNews.Identity.Services.Authentications.Validators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Identity.Services.Authentications.Handlers
{
    public class RefreshTokenResponse
    {
    }
    public class RefreshTokenRequest : IRequest<RefreshTokenResponse>
    {
    }
    public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly ILogger<RefreshTokenRequestHandler> _logger;
        private readonly IValidator<RefreshTokenValidator> _validator;

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new RefreshTokenValidatorDto() { };
            _validator.Validate(validatorDto);
            return new RefreshTokenResponse();
        }
    }
}
