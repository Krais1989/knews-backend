using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Posts.Entities;
using KNews.Core.Services.Posts.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostGetShortRequest : IRequest<PostShort>
    {
        public long PostID { get; set; }
        public long CurUserID { get; set; }
    }

    public class PostGetShortRequestHandler : IRequestHandler<PostGetShortRequest, PostShort>
    {
        private readonly ILogger<PostGetShortRequestHandler> _logger;
        private readonly IValidator<PostGetShortValidatorDto> _validator;
        private readonly CoreContext _context;

        public PostGetShortRequestHandler(ILogger<PostGetShortRequestHandler> logger, IValidator<PostGetShortValidatorDto> validator, CoreContext context)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<PostShort> Handle(PostGetShortRequest request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.AsNoTracking()
                            .Include(e => e.Community)
                            .FirstOrDefaultAsync(e => e.ID == request.PostID);

            var curUser = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.XCommunityUsers.FirstOrDefault(xcu => xcu.CommunityID == post.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.CurUserID);

            var validatorDto = new PostGetShortValidatorDto(
                getByAuthor: post.AuthorID == curUser.ID,
                postStatus: post.Status,
                communityReadPermissions: post.Community.ReadPermissions,
                curUserMembershipStatus: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
            );
            _validator.Validate(validatorDto);

            var response = new PostShort()
            {
                ID = post.ID,
                Title = post.Title,
                Content = post.Content,
                ShortContent = post.ShortContent
            };

            return response;
        }
    }
}