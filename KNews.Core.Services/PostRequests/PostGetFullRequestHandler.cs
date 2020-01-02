using FluentValidation;
using KNews.Core.Services.PostValidators;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KNews.Core.Services.PostRequests
{
    public class PostGetFullResponse
    {
    }
    public class PostGetFullRequest : IRequest<PostGetFullResponse>
    {
    }
    public class PostGetFullRequestHandler : IRequestHandler<PostGetFullRequest, PostGetFullResponse>
    {
        private readonly ILogger<PostGetFullRequestHandler> _logger;
        private readonly IValidator<PostGetFullValidator> _validator;

        public async Task<PostGetFullResponse> Handle(PostGetFullRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new PostGetFullValidatorDto() { };
            _validator.Validate(validatorDto);
            return new PostGetFullResponse();
        }
    }
}
