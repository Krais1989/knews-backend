using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostValidators
{
    public class PostDeleteValidatorDto
    {
    }

    public class PostDeleteValidator : AbstractValidator<PostDeleteValidatorDto>
    {
        public PostDeleteValidator()
        {
        }
    }

}
