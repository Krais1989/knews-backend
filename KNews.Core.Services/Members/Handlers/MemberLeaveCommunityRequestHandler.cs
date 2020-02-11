using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Communities.Validators;
using KNews.Core.Services.Members.Validators;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace KNews.Core.Services.Members.Handlers
{
    public class MemberLeaveCommunityResponse
    {
        public bool IsSuccess { get; }

        public MemberLeaveCommunityResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class MemberLeaveCommunityRequest : IRepositoryModifyRequest<MemberLeaveCommunityResponse>
    {
        public long UserID { get; set; }
        public long CommunityID { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class MemberLeaveCommunityRequestHandler : IRequestHandler<MemberLeaveCommunityRequest, MemberLeaveCommunityResponse>
    {
        private readonly CoreContext _context;
        private readonly ILogger<MemberLeaveCommunityRequestHandler> _logger;
        private readonly IValidator<MemberLeaveCommunityValidatorDto> _validator;

        public MemberLeaveCommunityRequestHandler(CoreContext context, ILogger<MemberLeaveCommunityRequestHandler> logger, IValidator<MemberLeaveCommunityValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<MemberLeaveCommunityResponse> Handle(MemberLeaveCommunityRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.XCommunityUsers.Where(cu => cu.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.UserID);
            var community = await _context.Communities.AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == request.CommunityID);

            var validationDto = new MemberLeaveCommunityValidatorDto(
                curUserMembershipStatus: user.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
                );

            await _validator.ValidateAsync(validationDto);
            await _context.XCommunityUsers.AddAsync(new XCommunityUser() { CommunityID = request.CommunityID, UserID = request.UserID });
            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            return new MemberLeaveCommunityResponse(true);
        }
    }
}