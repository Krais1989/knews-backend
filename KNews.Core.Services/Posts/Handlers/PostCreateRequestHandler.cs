using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Posts.Validators;
using KNews.Core.Services.Shared;
using KNews.Core.Services.Shared.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostCreateResponse
    {
        public Post Post { get; set; }
    }

    public class PostCreateRequest : IRepositoryModifyRequest<PostCreateResponse>
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public EPostStatus Status { get; set; }
        public long? CommunityID { get; set; }

        public int CurUserID { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class PostCreateRequestHandler : IRequestHandler<PostCreateRequest, PostCreateResponse>
    {
        private readonly NewsContext _context;
        private readonly ILogger<PostCreateRequestHandler> _logger;
        private readonly IValidator<PostCreateValidator> _validator;
        private readonly IOptions<CoreDomainOptions> _options;

        public PostCreateRequestHandler(
            NewsContext context,
            ILogger<PostCreateRequestHandler> logger,
            IValidator<PostCreateValidator> validator,
            IOptions<CoreDomainOptions> options)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
            _options = options;
        }

        public async Task<PostCreateResponse> Handle(PostCreateRequest request, CancellationToken cancellationToken)
        {
            var author = await _context.Users.FirstOrDefaultAsync(e => e.ID == request.CurUserID);
            var community = await _context.Communities.FirstOrDefaultAsync(e => e.ID == request.CommunityID);
            var authorInCommunity = await _context.XCommunityUsers.FirstOrDefaultAsync(e => e.CommunityID == request.CommunityID && e.UserID == request.CurUserID);

            var entity = new Post()
            {
                Title = request.Title,
                Content = request.Content,
                ShortContent = request.Content,
                CreateDate = DateTime.UtcNow,
                DeleteDate = null,
                UpdateDate = null,
                Status = EPostStatus.Created,
                AuthorID = request.CurUserID,
                CommunityID = request.CommunityID.HasValue ? request.CommunityID.Value : _options.Value.DefaultCommunityID
            };

            var validatorDto = new PostCreateValidatorDto()
            {
                CommunityCreatePermission = community.CreatePostPermissions,
                CommunityStatus = community.Status,
                AuthorStatus = author.Status,
                CurrentUserMembership = authorInCommunity != null ? authorInCommunity.Type : EXUserCommunityType.None,
                PostTitle = entity.Title,
                PostContent = entity.Content
            };

            _validator.Validate(validatorDto);

            _context.Posts.Add(entity);
            if (request.SaveChanges)
                await _context.SaveChangesAsync();

            return new PostCreateResponse() { Post = entity };
        }
    }
}