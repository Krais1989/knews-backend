using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Communities.Validators;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace KNews.Core.Services.Communities.Handlers
{
    public class CommunityUserJoinResponse
    {
        public bool IsSuccess { get; }

        public CommunityUserJoinResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class CommunityUserJoinRequest : IRepositoryModifyRequest<CommunityUserJoinResponse>
    {
        public long UserID { get; private set; }
        public long CommunityID { get; private set; }
        public bool SaveChanges { get; set; }

        public CommunityUserJoinRequest(long userId, long communityId)
        {
            UserID = userId;
            CommunityID = communityId;
        }
    }

    /// <summary>
    /// Вступить в группу
    /// </summary>
    public class CommunityUserJoinRequestHandler : IRequestHandler<CommunityUserJoinRequest, CommunityUserJoinResponse>
    {
        private readonly NewsContext _context;
        private readonly ILogger<CommunityUserJoinRequestHandler> _logger;
        private readonly IValidator<CommunityUserJoinValidatorDto> _validator;

        public CommunityUserJoinRequestHandler(NewsContext context, ILogger<CommunityUserJoinRequestHandler> logger, IValidator<CommunityUserJoinValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<CommunityUserJoinResponse> Handle(CommunityUserJoinRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.XCommunityUsers.Where(cu => cu.CommunityID == request.CommunityID))
                .Include(e => e.Invitations)
                .FirstOrDefaultAsync(e => e.ID == request.UserID);
            var community = await _context.Communities.AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == request.CommunityID);
            var validationDto = new CommunityUserJoinValidatorDto()
            {
                User = user,
                Community = community,
                UserInvitations = user.Invitations,
                XCommunityUsers = user.XCommunityUsers
            };

            await _validator.ValidateAsync(validationDto);
            await _context.XCommunityUsers.AddAsync(new XCommunityUser() { CommunityID = request.CommunityID, UserID = request.UserID });
            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new CommunityUserJoinResponse(true);
        }
    }
}