using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Shared;
using KNews.Core.Services.Users.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace KNews.Core.Services.Users.Handlers
{
    public class UserInviteCommunityResponse
    {
        public bool IsSuccess { get; private set; }

        public UserInviteCommunityResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class UserInviteCommunityRequest : IRepositoryModifyRequest<UserInviteCommunityResponse>
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

        public UserInviteCommunityRequest(long communityId, long invitedUserId, long invitingUserId)
        {
            CommunityID = communityId;
            TarUserID = invitedUserId;
            CurUserID = invitingUserId;
        }
    }

    /// <summary>
    /// Пригласить юзера
    /// </summary>
    public class UserInviteCommunityRequestHandler : IRequestHandler<UserInviteCommunityRequest, UserInviteCommunityResponse>
    {
        private readonly CoreContext _context;
        private readonly ILogger<UserInviteCommunityRequestHandler> _logger;
        private readonly IValidator<UserInviteCommunityValidatorDto> _validator;

        public UserInviteCommunityRequestHandler(CoreContext context,
            ILogger<UserInviteCommunityRequestHandler> logger,
            IValidator<UserInviteCommunityValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<UserInviteCommunityResponse> Handle(UserInviteCommunityRequest request, CancellationToken cancellationToken)
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

            var validationDto = new UserInviteCommunityValidatorDto(
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

            return new UserInviteCommunityResponse(true);
        }
    }
}