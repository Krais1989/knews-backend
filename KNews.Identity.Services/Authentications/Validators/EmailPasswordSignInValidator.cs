using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Identity.Services.Authentications.Validators
{
    public class EmailPasswordSignInValidatorDto
    {
    }

    public class EmailPasswordSignInValidator : AbstractValidator<EmailPasswordSignInValidatorDto>
    {
        public EmailPasswordSignInValidator()
        {
        }
    }
}
