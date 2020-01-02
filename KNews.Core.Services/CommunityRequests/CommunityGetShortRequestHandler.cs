using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.CommunityRequests
{
    public class CommunityShort
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ECommunityStatus Status { get; set; }
    }

    public class CommunityGetShortResponse
    {
        public CommunityShort[] Models { get; set; }
    }

    public class CommunityGetShortRequest : IRequest<CommunityGetShortResponse>
    {
        public long[] IDs { get; set; }
        public CommunityGetShortRequest(params long[] iDs)
        {
            IDs = iDs;
        }
    }

    public class CommunityGetShortRequestHandler : IRequestHandler<CommunityGetShortRequest, CommunityGetShortResponse>
    {
        private readonly NewsContext _context;
        private readonly IValidator<CommunityGetFullRequest> _validator;
        private readonly ILogger<CommunityGetFullRequestHandler> _logger;
        private readonly IEntityMapper<Community, CommunityShort> _communityShortMapper;

        public CommunityGetShortRequestHandler(NewsContext context, IValidator<CommunityGetFullRequest> validator, ILogger<CommunityGetFullRequestHandler> logger, IEntityMapper<Community, CommunityShort> communityShortMapper)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
            _communityShortMapper = communityShortMapper;
        }

        public async Task<CommunityGetShortResponse> Handle(CommunityGetShortRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request Execution");
            _validator.Validate(request);

            var response = new CommunityGetShortResponse();
            response.Models = await _context.Communities.AsNoTracking()
                .Select(e => _communityShortMapper.Map(e))
                .ToArrayAsync();
            return response;
        }
    }
}
