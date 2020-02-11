using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Authentications.Validators
{
    public class RefreshTokenValidatorDto
    {
    }

    public class RefreshTokenValidator : AbstractValidator<RefreshTokenValidatorDto>
    {
        public RefreshTokenValidator()
        {
        }
    }

}
