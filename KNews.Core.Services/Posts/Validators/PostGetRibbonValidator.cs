using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.Posts.Validators
{
    public class PostGetRibbonValidatorDto
    {
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class PostGetRibbonValidator : AbstractValidator<PostGetRibbonValidatorDto>
    {
        private readonly IOptions<CoreDomainOptions> _options;

        public PostGetRibbonValidator(IOptions<CoreDomainOptions> options)
        {
            _options = options;
        }

        public PostGetRibbonValidator()
        {
            RuleFor(dto => dto.Take).LessThanOrEqualTo(_options.Value.MaxRibbonPostsPerRequest);
        }

    }

}
