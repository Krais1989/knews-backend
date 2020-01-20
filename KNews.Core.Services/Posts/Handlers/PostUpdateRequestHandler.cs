using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Posts.Validators;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using System.Linq;
using Microsoft.Extensions.Options;

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostUpdateResponse
    {
    }

    public class PostUpdateRequest : IRepositoryModifyRequest<PostUpdateResponse>
    {
        public long ID { get; set; }
        public string NewTitle { get; set; }
        public string NewContent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public EPostStatus Status { get; set; }
        public long? CommunityID { get; set; }


        public int CurUserID { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class PostUpdateRequestHandler : IRequestHandler<PostUpdateRequest, PostUpdateResponse>
    {
        private readonly ILogger<PostUpdateRequestHandler> _logger;
        private readonly IValidator<PostUpdateValidator> _validator;
        private readonly CoreContext _context;
        private readonly IOptions<CoreDomainOptions> _options;

        public PostUpdateRequestHandler(
            ILogger<PostUpdateRequestHandler> logger,
            IValidator<PostUpdateValidator> validator,
            CoreContext context,
            IOptions<CoreDomainOptions> options)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
            _options = options;
        }

        public async Task<PostUpdateResponse> Handle(PostUpdateRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _context.Users.AsNoTracking()
                .IncludeFilter(u => u.XCommunityUsers.FirstOrDefault(xcu => xcu.CommunityID == request.CommunityID))
                .FirstOrDefaultAsync(e => e.ID == request.CurUserID);
            var post = await _context.Posts.Include(p => p.Community).FirstOrDefaultAsync();

            var validatorDto = new PostUpdateValidatorDto
            (
                newTitle : request.NewTitle,
                newContent: request.NewContent,
                postStatus: post.Status,
                postCreateDate: post.CreateDate,
                curUserStatus: curUser.Status,
                curUserIsAuthor: post.AuthorID == curUser.ID,
                curUserMembershipStatus: curUser.XCommunityUsers.FirstOrDefault()?.Status ?? EUserMembershipStatus.None
            );
            _validator.Validate(validatorDto);

            post.Title = request.NewTitle;
            post.Content = request.NewContent;

            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            return new PostUpdateResponse() { };
        }
    }
}