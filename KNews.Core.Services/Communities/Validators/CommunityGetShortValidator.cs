﻿using FluentValidation;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityGetShortValidator : AbstractValidator<CommunityGetShortRequest>
    {
        public CommunityGetShortValidator()
        {
            RuleFor(e => e.IDs).NotNull().Must(e => e.Length > 0 && e.Length < 1000);
        }
    }
}