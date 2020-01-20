using FluentValidation;
using KNews.Core.Persistence;
using KNews.Core.Services.Posts.Validators;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using System.Linq;
using KNews.Core.Entities;
using KNews.Core.Services.Posts.Entities;
using KNews.Core.Services.Shared.Mappers;

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostGetFullRequest : IRequest<PostFull>
    {
        public long PostID { get; set; }
        public long CurUserID { get; set; }
    }

    public class PostGetFullRequestHandler : IRequestHandler<PostGetFullRequest, PostFull>
    {
        private readonly IEntityMapper<Post, PostFull> _mapper;
        private readonly ILogger<PostGetFullRequestHandler> _logger;
        private readonly IValidator<PostGetFullValidator> _validator;
        private readonly CoreContext _context;

        public PostGetFullRequestHandler(
            IEntityMapper<Post, PostFull> mapper,
            ILogger<PostGetFullRequestHandler> logger,
            IValidator<PostGetFullValidator> validator,
            CoreContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<PostFull> Handle(PostGetFullRequest request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.AsNoTracking()
                .Include(e => e.Community)
                .FirstOrDefaultAsync(e => e.ID == request.PostID);

            var curUser = await _context.Users.AsNoTracking()
                .IncludeFilter(e => e.XCommunityUsers.FirstOrDefault(xcu => xcu.CommunityID == post.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.CurUserID);

            var validatorDto = new PostGetFullValidatorDto
            (
                getByAuthor: post.AuthorID == curUser.ID,
                postStatus: post.Status,
                communityReadPermissions: post.Community.ReadPermissions,
                curUserMembershipStatus: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
            );
            _validator.Validate(validatorDto);
            
            return _mapper.MapExpr.Compile()(post);
        }
    }
}