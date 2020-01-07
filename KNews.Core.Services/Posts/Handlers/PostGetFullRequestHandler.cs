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

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostGetFullRequest : IRequest<PostFull>
    {
        public long PostID { get; set; }
        public long CurUserID { get; set; }
    }

    public class PostGetFullRequestHandler : IRequestHandler<PostGetFullRequest, PostFull>
    {
        private readonly ILogger<PostGetFullRequestHandler> _logger;
        private readonly IValidator<PostGetFullValidator> _validator;
        private readonly NewsContext _context;

        public PostGetFullRequestHandler(ILogger<PostGetFullRequestHandler> logger, IValidator<PostGetFullValidator> validator, NewsContext context)
        {
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

            var validatorDto = new PostGetFullValidatorDto()
            {
                GetByAuthor = post.AuthorID == curUser.ID,
                PostStatus = post.Status,
                CommunityReadPermissions = post.Community.ReadPermissions,
                MemberStatus = curUser.XCommunityUsers != null ? (EXUserCommunityType?)curUser.XCommunityUsers.First().Type : null,
            };
            _validator.Validate(validatorDto);

            var response = new PostFull()
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