using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

using System.Linq;
using Z.EntityFramework.Plus;
using KNews.Core.Services.CommunityValidators;

namespace KNews.Core.Services.CommunityRequests
{
    public class CommunityUserInviteResponse
    {
        public bool IsSuccess { get; private set; }

        public CommunityUserInviteResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }

    public class CommunityUserInviteRequest : IRepositoryModifyRequest<CommunityUserInviteResponse>
    {
        public long CommunityID { get; private set; }
        /// <summary>
        /// Приглашаемый юзер
        /// </summary>
        public long InvitedUserID { get; private set; }
        /// <summary>
        /// Приглашающий юзер
        /// </summary>
        public long InvitingUserID { get; private set; }
        public bool SaveChanges { get; set; }

        public CommunityUserInviteRequest(long communityId, long invitedUserId, long invitingUserId)
        {
            CommunityID = communityId;
            InvitedUserID = invitedUserId;
            InvitingUserID = invitingUserId;
        }
    }

    /// <summary>
    /// Пригласить юзера
    /// </summary>
    public class CommunityUserInviteRequestHandler : IRequestHandler<CommunityUserInviteRequest, CommunityUserInviteResponse>
    {
        private readonly NewsContext _context;
        private readonly ILogger<CommunityUserInviteRequestHandler> _logger;
        private readonly IValidator<CommunityUserInviteValidatorDto> _validator;

        public CommunityUserInviteRequestHandler(NewsContext context,
            ILogger<CommunityUserInviteRequestHandler> logger,
            IValidator<CommunityUserInviteValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<CommunityUserInviteResponse> Handle(CommunityUserInviteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request Execution");

            var invitingUser = await _context.Users
                .IncludeFilter(u => u.CommunityUsers.Where(uc => uc.CommunityID == request.CommunityID))
                .Include(u => u.Invitations)
                .FirstOrDefaultAsync(e => e.ID == request.InvitingUserID);

            var invitedUser = await _context.Users
                .IncludeFilter(u => u.CommunityUsers.Where(uc => uc.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.InvitedUserID);

            var community = await _context.Communities.FirstOrDefaultAsync(e => e.ID == request.CommunityID);

            var validationDto = new CommunityUserInviteValidatorDto(
                invitingUser,
                invitedUser,
                community,
                invitedUser.Invitations,
                invitedUser.CommunityUsers.FirstOrDefault(),
                invitingUser.CommunityUsers.FirstOrDefault()
                );

            _validator.Validate(request);

            await _context.UserInvitations.AddAsync(new UserInvitation()
            {
                InvitedUserID = request.InvitedUserID,
                InvitingUserID = request.InvitingUserID,
                CommunityID = request.CommunityID,
                CreateDate = DateTime.UtcNow,
                Status = EUserInvitationStatus.Recieved
            });

            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            // Создание записи в БД
            // Отправка сообщения в Rabbit

            return new CommunityUserInviteResponse(true);
        }
    }
}
