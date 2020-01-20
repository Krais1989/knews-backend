using FluentValidation;
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
    public class CommentUpdateResponse
    {
    }
    public class CommentUpdateRequest : IRepositoryModifyRequest<CommentUpdateResponse>
    {
        public long CommentID { get; set; }
        public long CurUserID { get; set; }
        public string NewContent { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class CommentUpdateRequestHandler : IRequestHandler<CommentUpdateRequest, CommentUpdateResponse>
    {
        private readonly ILogger<CommentUpdateRequestHandler> _logger;
        private readonly IValidator<CommentUpdateValidator> _validator;
        private readonly CoreContext _context;

        public CommentUpdateRequestHandler(
            ILogger<CommentUpdateRequestHandler> logger,
            IValidator<CommentUpdateValidator> validator,
            CoreContext context)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<CommentUpdateResponse> Handle(CommentUpdateRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.ID == request.CurUserID);
            var comment = await _context.Comments
                .Include(c => c.Post)
                .Include(c => c.ParentComment)
                .FirstOrDefaultAsync(c => c.ID == request.CommentID);

            var validatorDto = new CommentUpdateValidatorDto
            (                
                commentCreateDate: comment.CreateDate,
                newContent: request.NewContent,
                authorStatus: curUser.Status,
                postStatus: comment.Post.Status,
                parentCommentStatus: comment.ParentComment?.Status,
                postCommentUpdatePermissions: comment.Post.CommentUpdatePermissions
            );
            _validator.Validate(validatorDto);

            comment.UpdateDate = DateTime.UtcNow;
            comment.Content = request.NewContent;
            comment.ShortContent = request.NewContent; // TODO: make short

            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            return new CommentUpdateResponse();
        }
    }
}
