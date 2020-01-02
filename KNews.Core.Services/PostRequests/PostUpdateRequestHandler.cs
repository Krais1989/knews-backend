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
    public class PostUpdateResponse
    {
    }
    public class PostUpdateRequest : IRequest<PostUpdateResponse>
    {
    }
    public class PostUpdateRequestHandler : IRequestHandler<PostUpdateRequest, PostUpdateResponse>
    {
        private readonly ILogger<PostUpdateRequestHandler> _logger;
        private readonly IValidator<PostUpdateValidator> _validator;

        public async Task<PostUpdateResponse> Handle(PostUpdateRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new PostUpdateValidatorDto() { };
            _validator.Validate(validatorDto);
            return new PostUpdateResponse();
        }
    }
}
