using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Members.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace KNews.Core.Services.Members.Handlers
{
    public class MemberExpelCommunityResponse
    {
    }
    public class MemberExpelCommunityRequest : IRequest<MemberExpelCommunityResponse>
    {
        public long CurUserID { get; set; }
        public long TarUserID { get; set; }
        public long CommunityID { get; set; }
    }
    public class MemberExpelCommunityRequestHandler : IRequestHandler<MemberExpelCommunityRequest, MemberExpelCommunityResponse>
    {
        private readonly ILogger<MemberExpelCommunityRequestHandler> _logger;
        private readonly IValidator<MemberExpelCommunityValidator> _validator;
        private readonly CoreContext _context;

        public MemberExpelCommunityRequestHandler(
            ILogger<MemberExpelCommunityRequestHandler> logger,
            IValidator<MemberExpelCommunityValidator> validator,
            CoreContext context)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<MemberExpelCommunityResponse> Handle(MemberExpelCommunityRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _context.Users
                .IncludeFilter(u => u.XCommunityUsers.Where(xcu => xcu.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(u => u.ID == request.CurUserID);
            var tarUser = await _context.Users
                .IncludeFilter(u => u.XCommunityUsers.Where(xcu => xcu.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(u => u.ID == request.TarUserID);
            var validatorDto = new MemberExpelCommunityValidatorDto(            
                curUserMembershipStatus: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None,
                tarUserMembershipStatus: tarUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
            );
            _validator.Validate(validatorDto);
            return new MemberExpelCommunityResponse();
        }
    }
}
