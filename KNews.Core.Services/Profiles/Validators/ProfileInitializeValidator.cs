using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Profiles.Validators
{
    public class ProfileInitializeValidatorDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string About { get; set; }

        public DateTime BirthDate { get; set; }
    }

    public class ProfileInitializeValidator : AbstractValidator<ProfileInitializeValidatorDto>
    {
        public ProfileInitializeValidator()
        {
        }
    }

}
