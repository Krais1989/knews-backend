using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostValidators
{

    public class PostUpdateValidatorDto
    {
    }

    public class PostUpdateValidator : AbstractValidator<PostUpdateValidatorDto>
    {
        public PostUpdateValidator()
        {
        }
    }


}
