using FluentValidation;
using KNews.Core.Persistence;
using KNews.Core.Services.Comments.Entities;
using KNews.Core.Services.Comments.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;
using KNews.Core.Services.Shared.Mappers;
using KNews.Core.Entities;

namespace KNews.Core.Services.Comments.Handlers
{
    public class CommentGetForPostResponse
    {
        public CommentForPost[] Data { get; set; }
    }
    public class CommentGetForPostRequest : IRequest<CommentGetForPostResponse>
    {
        public long? ParentCommentID { get; set; }
        public long CurUserID { get; set; }
        public long PostID { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }
    public class CommentGetForPostRequestHandler : IRequestHandler<CommentGetForPostRequest, CommentGetForPostResponse>
    {
        private readonly IEntityMapper<Comment, CommentForPost> _mapper;
        private readonly ILogger<CommentGetForPostRequestHandler> _logger;
        private readonly IValidator<CommentGetForPostValidator> _validator;
        private readonly CoreContext _context;

        public CommentGetForPostRequestHandler(
            IEntityMapper<Comment, CommentForPost> mapper,
            ILogger<CommentGetForPostRequestHandler> logger,
            IValidator<CommentGetForPostValidator> validator,
            CoreContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<CommentGetForPostResponse> Handle(CommentGetForPostRequest request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.AsNoTracking()
                .Include(p => p.Community)
                .FirstOrDefaultAsync(p => p.ID == request.PostID);

            var curUser = await _context.Users.AsNoTracking()
                .IncludeFilter(u => u.XCommunityUsers.Where(xcu => xcu.CommunityID == post.CommunityID))
                .FirstOrDefaultAsync(u => u.ID == request.CurUserID);

            var validatorDto = new CommentGetForPostValidatorDto
            (
                take: request.Take,
                curUserStatus: curUser.Status,
                postCommentReadPermissions: post.CommentReadPermissions,
                isCurUserAuthenticated: curUser != null,
                isCurUserCommunityMember: curUser.XCommunityUsers.Any()
            );
            _validator.Validate(validatorDto);

            var data = await _context.Comments.AsNoTracking()
                .Where(c => c.PostID == request.PostID)
                .Where(c => request.ParentCommentID == null || c.ParentCommentID == request.ParentCommentID)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(_mapper.MapExpr)
                .ToArrayAsync();

            return new CommentGetForPostResponse() { Data = data };
        }
    }
}
