using FluentValidation;
using KNews.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Profiles.Validators
{
    public class ProfileGetFullValidatorDto
    {
        public long TarUserID { get; set; }
        public long CurUserID { get; set; }

        public EUserStatus TarUserStatus { get; set; }
        public EUserStatus CurUserStatus { get; set; }

        
    }

    public class ProfileGetFullValidator : AbstractValidator<ProfileGetFullValidatorDto>
    {
        public ProfileGetFullValidator()
        {
        }
    }

}
