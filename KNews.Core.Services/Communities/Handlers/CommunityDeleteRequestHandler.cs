using FluentValidation;
using KNews.Core.Persistence;
using KNews.Core.Services.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Communities.Handlers
{
    public class CommunityDeleteResponse
    {
        public bool WasExisted { get; set; }

        public CommunityDeleteResponse(bool wasExisted)
        {
            WasExisted = wasExisted;
        }
    }

    public class CommunityDeleteRequest : IRepositoryModifyRequest<CommunityDeleteResponse>
    {
        public int ID { get; set; }
        public bool SaveChanges { get; set; }
    }

    public class CommunityDeleteRequestHandler : IRequestHandler<CommunityDeleteRequest, CommunityDeleteResponse>
    {
        private readonly CoreContext _context;
        private readonly IValidator<CommunityDeleteRequest> _validator;
        private readonly ILogger<CommunityDeleteRequestHandler> _logger;

        public CommunityDeleteRequestHandler(CoreContext context, IValidator<CommunityDeleteRequest> validator, ILogger<CommunityDeleteRequestHandler> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public async Task<CommunityDeleteResponse> Handle(CommunityDeleteRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request Execution");
            _validator.Validate(request);

            var entity = await _context.Communities.FirstOrDefaultAsync(e => e.ID == request.ID);
            if (entity == null) return new CommunityDeleteResponse(false);

            _context.Communities.Remove(entity);
            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new CommunityDeleteResponse(true);
        }
    }
}