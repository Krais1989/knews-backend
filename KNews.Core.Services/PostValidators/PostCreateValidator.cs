using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostValidators
{

    public class PostCreateValidatorDto
    {
    }

    public class PostCreateValidator : AbstractValidator<PostCreateValidatorDto>
    {
        public PostCreateValidator()
        {
        }
    }

}
