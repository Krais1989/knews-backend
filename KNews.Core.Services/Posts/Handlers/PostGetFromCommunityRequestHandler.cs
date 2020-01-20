using FluentValidation;
using KNews.Core.Entities;
using KNews.Core.Persistence;
using KNews.Core.Services.Posts.Entities;
using KNews.Core.Services.Posts.Validators;
using KNews.Core.Services.Shared.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.Posts.Handlers
{
    public class PostGetRibbonResponse
    {
    }

    public class PostGetRibbonRequest : IRequest<PostGetRibbonResponse>
    {
        public long? CurUserID { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class PostGetFromCommunityRequestHandler : IRequestHandler<PostGetRibbonRequest, PostGetRibbonResponse>
    {
        private readonly ILogger<PostGetFromCommunityRequestHandler> _logger;
        private readonly IValidator<PostGetRibbonValidator> _validator;
        private readonly CoreContext _context;
        private readonly IEntityMapper<Post, PostShort> _mapper;


        public async Task<PostGetRibbonResponse> Handle(PostGetRibbonRequest request, CancellationToken cancellationToken)
        {
            var curUser = request.CurUserID != null 
                ? _context.Users.FirstOrDefaultAsync(u => u.ID == request.CurUserID) 
                : null;

            var validatorDto = new PostGetRibbonValidatorDto() {
            };
            _validator.Validate(validatorDto);
            return new PostGetRibbonResponse();
        }
    }
}
