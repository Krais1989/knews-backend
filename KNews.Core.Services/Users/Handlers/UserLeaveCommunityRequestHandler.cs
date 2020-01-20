using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Communities.Validators;
using KNews.Core.Services.Shared;
using KNews.Core.Services.Users.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace KNews.Core.Services.Users.Handlers
{
    public class UserLeaveCommunityResponse
    {
        public bool IsSuccess { get; }

        public UserLeaveCommunityResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class UserLeaveCommunityRequest : IRepositoryModifyRequest<UserLeaveCommunityResponse>
    {
        public long UserID { get; set; }
        public long CommunityID { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class UserLeaveCommunityRequestHandler : IRequestHandler<UserLeaveCommunityRequest, UserLeaveCommunityResponse>
    {
        private readonly CoreContext _context;
        private readonly ILogger<UserLeaveCommunityRequestHandler> _logger;
        private readonly IValidator<UserLeaveCommunityValidatorDto> _validator;

        public UserLeaveCommunityRequestHandler(CoreContext context, ILogger<UserLeaveCommunityRequestHandler> logger, IValidator<UserLeaveCommunityValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<UserLeaveCommunityResponse> Handle(UserLeaveCommunityRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.XCommunityUsers.Where(cu => cu.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.UserID);
            var community = await _context.Communities.AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == request.CommunityID);

            var validationDto = new UserLeaveCommunityValidatorDto(
                curUserMembershipStatus: user.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
                );

            await _validator.ValidateAsync(validationDto);
            await _context.XCommunityUsers.AddAsync(new XCommunityUser() { CommunityID = request.CommunityID, UserID = request.UserID });
            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            return new UserLeaveCommunityResponse(true);
        }
    }
}