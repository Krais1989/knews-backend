using FluentValidation;
using KNews.Core.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Services.PostRibbons.Validators
{
    public class PostRibbonGetValidatorDto
    {
        public long Skip { get; private set; }
        public long Take { get; private set; }        
        public EUserStatus CurUserStatus { get; private set; }

        public PostRibbonGetValidatorDto(long skip, long take, EUserStatus curUserStatus)
        {
            Skip = skip;
            Take = take;
            CurUserStatus = curUserStatus;
        }
    }

    public class PostRibbonGetValidator : AbstractValidator<PostRibbonGetValidatorDto>
    {
        private readonly IOptions<CoreDomainOptions> _coreOptions;

        public PostRibbonGetValidator(IOptions<CoreDomainOptions> coreOptions)
        {
            _coreOptions = coreOptions;

            //RuleFor(dto => dto.CurUserStatus).Equal(EUserStatus.Approved).When(dto => dto.CurUserStatus != EUserStatus.None);
            RuleFor(dto => dto.Take).LessThanOrEqualTo(_coreOptions.Value.MaxRibbonPostsPerRequest);
        }
    }

}
