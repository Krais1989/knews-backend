using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Members.Validators;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace KNews.Core.Services.Members.Handlers
{
    public class MemberInviteCommunityResponse
    {
        public bool IsSuccess { get; private set; }

        public MemberInviteCommunityResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class MemberInviteCommunityRequest : IRepositoryModifyRequest<MemberInviteCommunityResponse>
    {
        public long CommunityID { get; private set; }

        /// <summary>
        /// Приглашаемый юзер
        /// </summary>
        public long TarUserID { get; private set; }

        /// <summary>
        /// Приглашающий юзер
        /// </summary>
        public long CurUserID { get; private set; }

        public bool SaveChanges { get; set; }

        public MemberInviteCommunityRequest(long communityId, long invitedUserId, long invitingUserId)
        {
            CommunityID = communityId;
            TarUserID = invitedUserId;
            CurUserID = invitingUserId;
        }
    }

    /// <summary>
    /// Пригласить юзера
    /// </summary>
    public class MemberInviteCommunityRequestHandler : IRequestHandler<MemberInviteCommunityRequest, MemberInviteCommunityResponse>
    {
        private readonly CoreContext _context;
        private readonly ILogger<MemberInviteCommunityRequestHandler> _logger;
        private readonly IValidator<MemberInviteCommunityValidatorDto> _validator;

        public MemberInviteCommunityRequestHandler(CoreContext context,
            ILogger<MemberInviteCommunityRequestHandler> logger,
            IValidator<MemberInviteCommunityValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<MemberInviteCommunityResponse> Handle(MemberInviteCommunityRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request Execution");

            var curUser = await _context.Users
                .IncludeFilter(u => u.XCommunityUsers.Where(uc => uc.CommunityID == request.CommunityID))
                .Include(u => u.Invitations)
                .FirstOrDefaultAsync(e => e.ID == request.CurUserID);

            var tarUser = await _context.Users
                .IncludeFilter(u => u.XCommunityUsers.Where(uc => uc.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.TarUserID);

            var community = await _context.Communities.FirstOrDefaultAsync(e => e.ID == request.CommunityID);

            var validationDto = new MemberInviteCommunityValidatorDto(
                curUserStatus: curUser.Status,
                targetUserStatus: tarUser.Status,
                communityStatus: community.Status,
                communityInvitationPermission: community.InvitationPermissions,
                curUserMembershipStatus: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None,
                tarUserMembershipStatus: tarUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None,
                tarUserInvitationStatus: tarUser.Invitations.FirstOrDefault()?.Status ?? EUserInvitationStatus.None
                );

            _validator.Validate(request);

            await _context.UserInvitations.AddAsync(new UserInvitation()
            {
                InvitedUserID = request.TarUserID,
                InvitingUserID = request.CurUserID,
                CommunityID = request.CommunityID,
                CreateDate = DateTime.UtcNow,
                Status = EUserInvitationStatus.Recieved
            });

            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            // Создание записи в БД
            // Отправка сообщения в Rabbit

            return new MemberInviteCommunityResponse(true);
        }
    }
}