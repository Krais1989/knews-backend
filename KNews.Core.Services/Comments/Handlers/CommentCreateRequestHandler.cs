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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Comments.Handlers
{
    public class CommentCreateResponse
    {
        public long ID { get; set; }
    }

    public class CommentCreateRequest : IRepositoryModifyRequest<CommentCreateResponse>
    {
        public long CurUserID { get; set; }
        public long PostID { get; set; }
        public long? ParentCommentID { get; set; }
        public string Content { get; set; }

        public bool SaveChanges { get; set; }
    }

    public class CommentCreateRequestHandler : IRequestHandler<CommentCreateRequest, CommentCreateResponse>
    {
        private readonly ILogger<CommentCreateRequestHandler> _logger;
        private readonly IValidator<CommentCreateValidator> _validator;
        private readonly CoreContext _context;

        public CommentCreateRequestHandler(
            ILogger<CommentCreateRequestHandler> logger,
            IValidator<CommentCreateValidator> validator,
            CoreContext context)
        {
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<CommentCreateResponse> Handle(CommentCreateRequest request, CancellationToken cancellationToken)
        {            
            var curUser = await _context.Users.FirstOrDefaultAsync(u => u.ID == request.CurUserID);
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.ID == request.PostID);
            var parentComment = request.ParentCommentID.HasValue
                ? await _context.Comments.FirstOrDefaultAsync(c => c.ID == request.ParentCommentID)
                : null;

            var validatorDto = new CommentCreateValidatorDto
            (                
                content: request.Content,
                authorStatus: curUser.Status,
                postStatus: post.Status,
                parentCommentStatus: parentComment != null ? (ECommentStatus?)parentComment.Status : null,
                commentCreatePermissions: post.CommentCreatePermissions
            );
            _validator.Validate(validatorDto);

            var entity = new Comment()
            {
                ID = 0,
                AuthorID = request.CurUserID,
                PostID = request.PostID,
                ParentCommentID = request.ParentCommentID,
                Status = ECommentStatus.Posted,
                CreateDate = DateTime.UtcNow,
                Content = request.Content,
                ShortContent = request.Content, // Generate
            };
            _context.Add(entity);

            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new CommentCreateResponse() { ID = entity.ID };
        }
    }
}
