using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
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
    public class UserJoinCommunityResponse
    {
        public bool IsSuccess { get; }

        public UserJoinCommunityResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class UserJoinCommunityRequest : IRepositoryModifyRequest<UserJoinCommunityResponse>
    {
        public long CurUserID { get; private set; }
        public long CommunityID { get; private set; }
        public bool SaveChanges { get; set; }

        public UserJoinCommunityRequest(long userId, long communityId)
        {
            CurUserID = userId;
            CommunityID = communityId;
        }
    }

    /// <summary>
    /// Вступить в группу
    /// </summary>
    public class UserJoinCommunityRequestHandler : IRequestHandler<UserJoinCommunityRequest, UserJoinCommunityResponse>
    {
        private readonly CoreContext _context;
        private readonly ILogger<UserJoinCommunityRequestHandler> _logger;
        private readonly IValidator<UserJoinCommunityValidatorDto> _validator;

        public UserJoinCommunityRequestHandler(CoreContext context, ILogger<UserJoinCommunityRequestHandler> logger, IValidator<UserJoinCommunityValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<UserJoinCommunityResponse> Handle(UserJoinCommunityRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.XCommunityUsers.Where(cu => cu.CommunityID == request.CommunityID))
                .IncludeFilter(e => e.Invitations.Where(i => i.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.CurUserID);
            var community = await _context.Communities.AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == request.CommunityID);

            var validationDto = new UserJoinCommunityValidatorDto(
                curUserStatus: curUser.Status,
                communityStatus: community.Status,
                communityJoinPermissions: community.JoinPermissions,
                curUserMembershipStatus: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None,
                curUserInvitationStatus: curUser.Invitations.FirstOrDefault()?.Status ?? EUserInvitationStatus.None
                );

            await _validator.ValidateAsync(validationDto);
            await _context.XCommunityUsers.AddAsync(new XCommunityUser() { CommunityID = request.CommunityID, UserID = request.CurUserID });
            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new UserJoinCommunityResponse(true);
        }
    }
}