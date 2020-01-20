using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Communities.Handlers
{
    public class CommunityCreateResponse
    {
        public long CommunityID { get; set; }

        public CommunityCreateResponse(long newId)
        {
            CommunityID = newId;
        }
    }

    public class CommunityCreateRequest : IRepositoryModifyRequest<CommunityCreateResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public ECommunityStatus Status { get; set; }
        public long OwnerID { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class CommunityCreateRequestHandler : IRequestHandler<CommunityCreateRequest, CommunityCreateResponse>
    {
        private readonly CoreContext _context;
        private readonly IValidator<CommunityCreateRequest> _validator;
        private readonly ILogger<CommunityCreateRequestHandler> _logger;

        public CommunityCreateRequestHandler(CoreContext context, IValidator<CommunityCreateRequest> validator, ILogger<CommunityCreateRequestHandler> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public async Task<CommunityCreateResponse> Handle(CommunityCreateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request Execution");
            _validator.Validate(request);

            var entity = new Core.Entities.Community()
            {
                ID = 0,
                Title = request.Title,
                Description = request.Description,
                Rules = request.Rules,
                Status = ECommunityStatus.Created,
            };

            await _context.AddAsync(entity, cancellationToken);
            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new CommunityCreateResponse(entity.ID);
        }
    }
}