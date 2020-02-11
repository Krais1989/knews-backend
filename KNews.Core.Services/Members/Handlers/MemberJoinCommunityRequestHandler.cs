using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
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
    public class MemberJoinCommunityResponse
    {
        public bool IsSuccess { get; }

        public MemberJoinCommunityResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class MemberJoinCommunityRequest : IRepositoryModifyRequest<MemberJoinCommunityResponse>
    {
        public long CurUserID { get; private set; }
        public long CommunityID { get; private set; }
        public bool SaveChanges { get; set; }

        public MemberJoinCommunityRequest(long userId, long communityId)
        {
            CurUserID = userId;
            CommunityID = communityId;
        }
    }

    /// <summary>
    /// Вступить в группу
    /// </summary>
    public class MemberJoinCommunityRequestHandler : IRequestHandler<MemberJoinCommunityRequest, MemberJoinCommunityResponse>
    {
        private readonly CoreContext _context;
        private readonly ILogger<MemberJoinCommunityRequestHandler> _logger;
        private readonly IValidator<MemberJoinCommunityValidatorDto> _validator;

        public MemberJoinCommunityRequestHandler(CoreContext context, ILogger<MemberJoinCommunityRequestHandler> logger, IValidator<MemberJoinCommunityValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<MemberJoinCommunityResponse> Handle(MemberJoinCommunityRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.XCommunityUsers.Where(cu => cu.CommunityID == request.CommunityID))
                .IncludeFilter(e => e.Invitations.Where(i => i.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.CurUserID);
            var community = await _context.Communities.AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == request.CommunityID);

            var validationDto = new MemberJoinCommunityValidatorDto(
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
            return new MemberJoinCommunityResponse(true);
        }
    }
}