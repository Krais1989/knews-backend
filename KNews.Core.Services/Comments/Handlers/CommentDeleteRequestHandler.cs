using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Comments.Validators;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Comments.Handlers
{
    public class CommentDeleteResponse
    {
    }
    public class CommentDeleteRequest : IRepositoryModifyRequest<CommentDeleteResponse>
    {
        public long CurUserID { get; set; }
        public long CommentID { get; set; }
        public bool SaveChanges { get; set; }
    }
    public class CommentDeleteRequestHandler : IRequestHandler<CommentDeleteRequest, CommentDeleteResponse>
    {
        private readonly ILogger<CommentDeleteRequestHandler> _logger;
        private readonly IValidator<CommentDeleteValidator> _validator;
        private readonly CoreContext _context;

        public CommentDeleteRequestHandler(
            ILogger<CommentDeleteRequestHandler> logger,
            IValidator<CommentDeleteValidator> validator,
            CoreContext context)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<CommentDeleteResponse> Handle(CommentDeleteRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _context.Users.FirstOrDefaultAsync(u => u.ID == request.CurUserID);
            var comment = await _context.Comments.Include(c => c.Post).FirstOrDefaultAsync(c => c.ID == request.CommentID);
            var validatorDto = new CommentDeleteValidatorDto(
                commentStatus: comment.Status,
                curUserStatus: curUser.Status,
                postCommentDeletePermissions: comment.Post.CommentDeletePermissions
            );
            _validator.Validate(validatorDto);
            comment.Status = ECommentStatus.Deleted;
            comment.DeleteDate = DateTime.UtcNow;
            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new CommentDeleteResponse();
        }
    }
}
