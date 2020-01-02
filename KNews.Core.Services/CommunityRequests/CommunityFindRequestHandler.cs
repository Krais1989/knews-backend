using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.CommunityRequests
{
    public class CommunityFindResponse
    {
        public IEnumerable<Community> Result { get; set; }
    }

    public class CommunityFindRequest : IRequest<CommunityFindResponse>
    {
        public string Text { get; set; }
        /// <summary>
        /// Группы в которых состоит пользователь
        /// </summary>
        public int? UserId { get; set; }
        public string[] Tags { get; set; }
        public bool? OrderByMembers { get; set; }
        public bool? OrderByCreateDate { get; set; }
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }

    public class CommunityFindRequestHandler : IRequestHandler<CommunityFindRequest, CommunityFindResponse>
    {
        private readonly NewsContext _context;
        private readonly ILogger<CommunityFindRequestHandler> _logger;
        private readonly IValidator<CommunityFindRequest> _validator;
        private readonly IDistributedCache _cache;

        public CommunityFindRequestHandler(NewsContext context, ILogger<CommunityFindRequestHandler> logger, IValidator<CommunityFindRequest> validator, IDistributedCache cache)
        {
            _context = context;
            _logger = logger;
            _validator = validator;
            _cache = cache;
        }

        public async Task<CommunityFindResponse> Handle(CommunityFindRequest request, CancellationToken cancellationToken)
        {
            if (request.Skip == null)
                request.Skip = 0;
            if (request.Take == null)
                request.Take = 100;

            _logger.LogInformation($"Request Execution");
            _validator.Validate(request);

            string requestCacheKey = "";
            var cachedRawResponse = await _cache.GetStringAsync(requestCacheKey);
            if (!string.IsNullOrEmpty(cachedRawResponse))
                return JsonSerializer.Deserialize<CommunityFindResponse>(cachedRawResponse);

            var query = _context.Communities.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(request.Text))
                query = query.Where(e => e.Title.Contains(request.Text) || e.Description.Contains(request.Text));
            if (request.UserId != null)
                query = query.Where(e => e.UserCommunities.Any(ee => ee.UserID == request.UserId));
            if (request.OrderByCreateDate != null)
                query = query.OrderByDescending(e => e.CreateDate);
            if (request.OrderByMembers != null)
                query = query.OrderByDescending(e => e.MembersCount);
            query = query.Skip(request.Skip.Value).Take(request.Take.Value);

            var response = new CommunityFindResponse();
            response.Result = await query.ToListAsync();

            var cacheOpt = new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(3) };
            await _cache.SetStringAsync(requestCacheKey, JsonSerializer.Serialize(response), cacheOpt);

            return response;
        }
    }


}
