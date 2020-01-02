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
    public class PostFindResponse
    {
    }
    public class PostFindRequest : IRequest<PostFindResponse>
    {
    }
    public class PostFindRequestHandler : IRequestHandler<PostFindRequest, PostFindResponse>
    {
        private readonly ILogger<PostFindRequestHandler> _logger;
        private readonly IValidator<PostFindValidator> _validator;

        public async Task<PostFindResponse> Handle(PostFindRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new PostFindValidatorDto() { };
            _validator.Validate(validatorDto);
            return new PostFindResponse();
        }
    }
}
