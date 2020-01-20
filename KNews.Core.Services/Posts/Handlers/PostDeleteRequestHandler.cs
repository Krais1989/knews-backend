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

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostDeleteResponse
    {
    }

    public class PostDeleteRequest : IRepositoryModifyRequest<PostDeleteResponse>
    {
        /// <summary>
        /// ID удаляемого поста
        /// </summary>
        public int PostID { get; set; }
        /// <summary>
        /// ID удаляющего юзера
        /// </summary>
        public int UserID { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class PostDeleteRequestHandler : IRequestHandler<PostDeleteRequest, PostDeleteResponse>
    {
        private readonly ILogger<PostDeleteRequestHandler> _logger;
        private readonly IValidator<PostDeleteValidator> _validator;
        private readonly CoreContext _context;

        public PostDeleteRequestHandler(ILogger<PostDeleteRequestHandler> logger, IValidator<PostDeleteValidator> validator, CoreContext context)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<PostDeleteResponse> Handle(PostDeleteRequest request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts
                .Include(p => p.Community)
                .FirstOrDefaultAsync(p => p.ID == request.PostID);

            var curUser = await _context.Users.AsNoTracking()
                .IncludeFilter(u => u.XCommunityUsers.FirstOrDefault(xcu => xcu.CommunityID == post.CommunityID))
                .FirstOrDefaultAsync();

            var validatorDto = new PostDeleteValidatorDto
            (
                commPostDeletePermission: post.Community.DeletePermissions,
                postCreateDate: post.CreateDate,
                curUserIsAuthor: curUser.ID == post.AuthorID,
                curUserStatus: curUser.Status,
                curUserMembership: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
            );
            _validator.Validate(validatorDto);

            _context.Remove(post);
            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            return new PostDeleteResponse();
        }
    }
}