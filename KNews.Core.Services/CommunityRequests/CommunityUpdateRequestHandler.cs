using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.CommunityRequests
{
    public class CommunityUpdateResponse
    {

    }

    public class CommunityUpdateRequest : IRepositoryModifyRequest<CommunityUpdateResponse>
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rules { get; set; }
        public ECommunityStatus? Status { get; set; }

        public bool SaveChanges { get; set; }
    }

    public class CommunityUpdateRequestHandler : IRequestHandler<CommunityUpdateRequest, CommunityUpdateResponse>
    {
        private readonly NewsContext _context;
        private readonly IValidator<CommunityUpdateRequest> _validator;
        private readonly ILogger<CommunityUpdateRequestHandler> _logger;

        public CommunityUpdateRequestHandler(NewsContext context, IValidator<CommunityUpdateRequest> validator, ILogger<CommunityUpdateRequestHandler> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public async Task<CommunityUpdateResponse> Handle(CommunityUpdateRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request Handler");
            _validator.Validate(request);            

            var entity = await _context.Communities.FirstOrDefaultAsync(e => e.ID == request.ID);
            entity.Title = request.Title;
            entity.Description = request.Description;
            entity.Rules = request.Rules;
            entity.Status = request.Status.Value;

            _context.Update(entity);
            if (request.SaveChanges)
                await _context.SaveChangesAsync();
            return new CommunityUpdateResponse();
        }
    }
}
