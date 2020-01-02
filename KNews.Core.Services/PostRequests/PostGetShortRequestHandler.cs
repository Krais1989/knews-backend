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
    public class PostGetShortResponse
    {
    }
    public class PostGetShortRequest : IRequest<PostGetShortResponse>
    {
    }
    public class PostGetShortRequestHandler : IRequestHandler<PostGetShortRequest, PostGetShortResponse>
    {
        private readonly ILogger<PostGetShortRequestHandler> _logger;
        private readonly IValidator<PostGetShortValidator> _validator;

        public async Task<PostGetShortResponse> Handle(PostGetShortRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new PostGetShortValidatorDto() { };
            _validator.Validate(validatorDto);
            return new PostGetShortResponse();
        }
    }
}
