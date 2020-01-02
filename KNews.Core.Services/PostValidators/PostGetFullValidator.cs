using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostValidators
{
    public class PostGetFullValidatorDto
    {
    }

    public class PostGetFullValidator : AbstractValidator<PostGetFullValidatorDto>
    {
        public PostGetFullValidator()
        {
        }
    }

}
