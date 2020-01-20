using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Posts.Entities;
using KNews.Core.Services.Posts.Validators;
using KNews.Core.Services.Shared.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostFindResponse
    {
        public PostShort[] Data { get; set; }
    }

    public class PostFindRequest : IRequest<PostFindResponse>
    {
        public string Text { get; set; }
        public long? AuthorID { get; set; }
        public long? CommunityID { get; set; }
    }

    public class PostFindRequestHandler : IRequestHandler<PostFindRequest, PostFindResponse>
    {
        private readonly IEntityMapper<Post, PostShort> _mapper;
        private readonly ILogger<PostFindRequestHandler> _logger;
        private readonly IValidator<PostFindValidator> _validator;
        private readonly CoreContext _context;

        public PostFindRequestHandler(
            IEntityMapper<Post, PostShort> mapper,
            ILogger<PostFindRequestHandler> logger,
            IValidator<PostFindValidator> validator,
            CoreContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<PostFindResponse> Handle(PostFindRequest request, CancellationToken cancellationToken)
        {
            var curUser = request.AuthorID.HasValue
                ? await _context.Users.FirstOrDefaultAsync(e => e.ID == request.AuthorID)
                : null;

            var validatorDto = new PostFindValidatorDto
            (
                text: request.Text
            );
            _validator.Validate(validatorDto);

            var query = _context.Posts.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(request.Text))
                query = query.Where(p => p.Title.Contains(request.Text) || p.Content.Contains(request.Text));
            if (request.AuthorID != null)
                query = query.Where(p => p.AuthorID == request.AuthorID);
            if (request.CommunityID != null)
                query = query.Where(p => p.CommunityID == request.CommunityID);

            var data = await query.Select(_mapper.MapExpr).ToArrayAsync();

            return new PostFindResponse() { Data = data };
        }
    }
}