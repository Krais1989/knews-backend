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
        public long? UserID { get; set; }
    }

    public class PostFindRequestHandler : IRequestHandler<PostFindRequest, PostFindResponse>
    {
        private readonly IEntityMapper<Post, PostShort> _mapper;
        private readonly ILogger<PostFindRequestHandler> _logger;
        private readonly IValidator<PostFindValidator> _validator;
        private readonly NewsContext _context;

        public PostFindRequestHandler(
            IEntityMapper<Post, PostShort> mapper,
            ILogger<PostFindRequestHandler> logger,
            IValidator<PostFindValidator> validator,
            NewsContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
            _context = context;
        }

        public async Task<PostFindResponse> Handle(PostFindRequest request, CancellationToken cancellationToken)
        {
            var curUser = request.UserID.HasValue
                ? await _context.Users.FirstOrDefaultAsync(e => e.ID == request.UserID)
                : null;

            var validatorDto = new PostFindValidatorDto()
            {
                Text = request.Text,
                CurUserStatus = curUser != null ? (EUserStatus?)curUser.Status : null,
                IsAnonymous = curUser == null
            };
            _validator.Validate(validatorDto);

            var data = await _context.Posts.AsNoTracking()
                .Where(p => p.Title.Contains(request.Text) || p.Content.Contains(request.Text))
                .Select(p => _mapper.Map(p))
                .ToArrayAsync();

            return new PostFindResponse() { Data = data };
        }
    }
}