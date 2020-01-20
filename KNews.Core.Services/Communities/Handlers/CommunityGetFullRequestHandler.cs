﻿using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Communities.Entities;
using KNews.Core.Services.Shared.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Communities.Handlers
{
    public class CommunityGetFullResponse
    {
        public CommunityFull[] Models { get; set; }
    }

    public class CommunityGetFullRequest : IRequest<CommunityGetFullResponse>
    {
        public long[] IDs { get; set; }

        public CommunityGetFullRequest(params long[] id)
        {
            IDs = id;
        }
    }

    public class CommunityGetFullRequestHandler : IRequestHandler<CommunityGetFullRequest, CommunityGetFullResponse>
    {
        private readonly CoreContext _context;
        private readonly IValidator<CommunityGetFullRequest> _validator;
        private readonly ILogger<CommunityGetFullRequestHandler> _logger;
        private readonly IEntityMapper<Community, CommunityFull> _communityFullMapper;

        public CommunityGetFullRequestHandler(CoreContext context, IValidator<CommunityGetFullRequest> validator, ILogger<CommunityGetFullRequestHandler> logger)
        {
            _context = context;
            _validator = validator;
            _logger = logger;
        }

        public async Task<CommunityGetFullResponse> Handle(CommunityGetFullRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Request Execution");
            _validator.Validate(request);

            // Нельзя возвращать сам Entity, нужно преобразовать его в DTO (в CommunityGetResponse)
            // Нужно определиться какие проекции возвращает Api, например полное/кратное описание комьюнити и для каждого сделать отдельный RequestHandler

            var response = new CommunityGetFullResponse();
            response.Models = await _context.Communities.AsNoTracking()
                .Where(e => request.IDs.Contains(e.ID))
                .Select(_communityFullMapper.MapExpr)
                .ToArrayAsync();

            return response;
        }
    }
}