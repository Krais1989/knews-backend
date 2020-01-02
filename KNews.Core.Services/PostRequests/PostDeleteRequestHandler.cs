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
    public class PostDeleteResponse
    {
    }
    public class PostDeleteRequest : IRequest<PostDeleteResponse>
    {
    }
    public class PostDeleteRequestHandler : IRequestHandler<PostDeleteRequest, PostDeleteResponse>
    {
        private readonly ILogger<PostDeleteRequestHandler> _logger;
        private readonly IValidator<PostDeleteValidator> _validator;

        public async Task<PostDeleteResponse> Handle(PostDeleteRequest request, CancellationToken cancellationToken)
        {
            var validatorDto = new PostDeleteValidatorDto() { };
            _validator.Validate(validatorDto);
            return new PostDeleteResponse();
        }
    }

}
