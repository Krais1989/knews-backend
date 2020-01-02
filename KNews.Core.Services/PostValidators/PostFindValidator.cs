using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostValidators
{
    public class PostFindValidatorDto
    {
    }

    public class PostFindValidator : AbstractValidator<PostFindValidatorDto>
    {
        public PostFindValidator()
        {
        }
    }

}
