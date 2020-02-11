using FluentValidation;
using KNews.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Profiles.Validators
{
    public class ProfileDeleteValidatorDto
    {
        public long CurUserID { get; set; }
        public EUserStatus CurUserStatus { get; set; }
    }

    public class ProfileDeleteValidator : AbstractValidator<ProfileDeleteValidatorDto>
    {
        public ProfileDeleteValidator()
        {
        }
    }

}
