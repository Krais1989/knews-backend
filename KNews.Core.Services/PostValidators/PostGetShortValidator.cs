using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostValidators
{

    public class PostGetShortValidatorDto
    {
    }

    public class PostGetShortValidator : AbstractValidator<PostGetShortValidatorDto>
    {
        public PostGetShortValidator()
        {
        }
    }

}
