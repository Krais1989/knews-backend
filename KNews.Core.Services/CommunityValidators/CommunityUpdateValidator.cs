using FluentValidation;
using FluentValidation.Results;
using KNews.Core.Services.CommunityRequests;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.CommunityValidators
{
    public class CommunityUpdateValidator : AbstractValidator<CommunityUpdateRequest>
    {
        public CommunityUpdateValidator()
        {
            RuleFor(e => e.ID).GreaterThan(0);
        }
    }
}
