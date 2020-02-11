using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.PostRibbons.Entities;
using KNews.Core.Services.PostRibbons.Validators;
using KNews.Core.Services.Shared.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.PostRibbons.Handlers
{
    public class PostRibbonGetResponse
    {
        public PostRibbon[] Data { get; set; }
    }
    public class PostRibbonGetRequest : IRequest<PostRibbonGetResponse>
    {
        public long CurUserID { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
    }

    public class PostRibbonGetRequestHandler : IRequestHandler<PostRibbonGetRequest, PostRibbonGetResponse>
    {
        private readonly ILogger<PostRibbonGetRequestHandler> _logger;
        private readonly IValidator<PostRibbonGetValidator> _validator;
        private readonly CoreContext _coreContext;
        private readonly IEntityMapper<Post, PostRibbon> _mapper;

        public PostRibbonGetRequestHandler(
            ILogger<PostRibbonGetRequestHandler> logger,
            IValidator<PostRibbonGetValidator> validator,
            CoreContext coreContext,
            IEntityMapper<Post, PostRibbon> mapper)
        {
            _logger = logger;
            _validator = validator;
            _coreContext = coreContext;
            _mapper = mapper;
        }

        public async Task<PostRibbonGetResponse> Handle(PostRibbonGetRequest request, CancellationToken cancellationToken)
        {
            var curUser = await _coreContext.Users.FirstOrDefaultAsync(u => u.ID == request.CurUserID);
            var validatorDto = new PostRibbonGetValidatorDto(
                skip: request.Skip,
                take: request.Take,
                curUserStatus: curUser.Status);

            _validator.Validate(validatorDto);

            var postRibbons = await _coreContext.Posts.AsNoTracking()
                .Include(p => p.Author)
                .Include(p => p.Community)
                .OrderByDescending(p => p.CreateDate)
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(_mapper.MapExpr)
                .ToArrayAsync();

            return new PostRibbonGetResponse() { Data = postRibbons };
        }
    }
}
