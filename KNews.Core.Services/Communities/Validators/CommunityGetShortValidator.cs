using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Services.Communities.Handlers;

namespace KNews.Core.Services.Communities.Validators
{
    public class CommunityGetShortValidatorDto 
    {
        public ECommunityStatus CommunityStatus { get; set; }
    };

    public class CommunityGetShortValidator : AbstractValidator<CommunityGetShortValidatorDto>
    {
        public CommunityGetShortValidator()
        {
            RuleFor(dto => dto.CommunityStatus).NotEqual(ECommunityStatus.Deleted);
        }
    }
}
