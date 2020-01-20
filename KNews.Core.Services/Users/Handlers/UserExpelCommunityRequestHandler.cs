using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Users.Validators;
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

namespace KNews.Core.Services.Users.Handlers
{
    public class UserExpelCommunityResponse
    {
    }
    public class UserExpelCommunityRequest : IRequest<UserExpelCommunityResponse>
    {
        public long CurUserID { get; set; }
        public long TarUserID { get; set; }
        public long CommunityID { get; set; }
    }
    public class UserExpelCommunityRequestHandler : IRequestHandler<UserExpelCommunityRequest, UserExpelCommunityResponse>
    {
        private readonly ILogger<UserExpelCommunityRequestHandler> _logger;
        private readonly IValidator<UserExpelCommunityValidator> _validator;
        private readonly CoreContext _context;

        public UserExpelCommunityRequestHandler(
            ILogger<UserExpelCommunityRequestHandler> logger,
            IValidator<UserExpelCommunityValidator> validator,
            CoreContext context)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<UserExpelCommunityResponse> Handle(UserExpelCommunityRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _context.Users
                .IncludeFilter(u => u.XCommunityUsers.Where(xcu => xcu.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(u => u.ID == request.CurUserID);
            var tarUser = await _context.Users
                .IncludeFilter(u => u.XCommunityUsers.Where(xcu => xcu.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(u => u.ID == request.TarUserID);
            var validatorDto = new UserExpelCommunityValidatorDto(            
                curUserMembershipStatus: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None,
                tarUserMembershipStatus: tarUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
            );
            _validator.Validate(validatorDto);
            return new UserExpelCommunityResponse();
        }
    }
}
