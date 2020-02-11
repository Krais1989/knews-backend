using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Profiles.Validators
{
    public class ProfileUpdateValidatorDto
    {
    }

    public class ProfileUpdateValidator : AbstractValidator<ProfileUpdateValidatorDto>
    {
        public ProfileUpdateValidator()
        {
        }
    }

}
