
using FluentValidation;
using KNews.Core.Services.PostValidators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.PostRequests
{

    public class PostCreateResponse
    {
    }
    public class PostCreateRequest : IRequest<PostCreateResponse>
    {
    }
    public class PostCreateRequestHandler : IRequestHandler<PostCreateRequest, PostCreateResponse>
    {
        private readonly ILogger<PostCreateRequestHandler> _logger;
        private readonly IValidator<PostCreateValidator> _validator;

        public PostCreateRequestHandler(ILogger<PostCreateRequestHandler> logger, IValidator<PostCreateValidator> validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public async Task<PostCreateResponse> Handle(PostCreateRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new PostCreateValidatorDto() { };
            _validator.Validate(validatorDto);

            return new PostCreateResponse();
        }
    }
}