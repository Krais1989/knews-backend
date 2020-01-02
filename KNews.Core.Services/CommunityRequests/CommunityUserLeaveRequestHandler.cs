using FluentValidation;
using KNews.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using System.Linq;
using KNews.Core.Entities;
using KNews.Core.Services.CommunityValidators;

namespace KNews.Core.Services.CommunityRequests
{
    public class CommunityUserLeaveResponse
    {
        public bool IsSuccess { get; }

        public CommunityUserLeaveResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
    }
    public class CommunityUserLeaveRequest : IRepositoryModifyRequest<CommunityUserLeaveResponse>
    {
        public long UserID { get; set; }
        public long CommunityID { get; set; }
        public bool SaveChanges { get; set; }
    }
    public class CommunityUserLeaveRequestHandler : IRequestHandler<CommunityUserLeaveRequest, CommunityUserLeaveResponse>
    {
        private readonly NewsContext _context;
        private readonly ILogger<CommunityUserLeaveRequestHandler> _logger;
        private readonly IValidator<CommunityUserLeaveValidatorDto> _validator;

        public CommunityUserLeaveRequestHandler(NewsContext context, ILogger<CommunityUserLeaveRequestHandler> logger, IValidator<CommunityUserLeaveValidatorDto> validator)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
        }

        public async Task<CommunityUserLeaveResponse> Handle(CommunityUserLeaveRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.CommunityUsers.Where(cu => cu.CommunityID == request.CommunityID))
                .Include(e => e.Invitations)
                .FirstOrDefaultAsync(e => e.ID == request.UserID);
            var community = await _context.Communities.AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == request.CommunityID);
            var validationDto = new CommunityUserLeaveValidatorDto(user, community, user.CommunityUsers);

            await _validator.ValidateAsync(validationDto);
            await _context.XCommunityUsers.AddAsync(new XCommunityUser() { CommunityID = request.CommunityID, UserID = request.UserID });
            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new CommunityUserLeaveResponse(true);
        }
    }
}
