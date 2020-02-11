using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Profiles.Validators
{
    public class ProfileBanValidatorDto
    {
    }

    public class ProfileBanValidator : AbstractValidator<ProfileBanValidatorDto>
    {
        public ProfileBanValidator()
        {
        }
    }


}
