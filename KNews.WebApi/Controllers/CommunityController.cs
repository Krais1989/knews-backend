using KNews.Core.Services.Communities.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KNews.Post.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : BaseController
    {
        public CommunityController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFull(long id)
        {
            var result = await _mediator.Send(new CommunityGetFullRequest(id)).ConfigureAwait(false);
            return Ok(result);
        }

        public async Task<IActionResult> GetShort(long id)
        {
            var result = await _mediator.Send(new CommunityGetShortRequest(id)).ConfigureAwait(false);
            return Ok(result);
        }

        public async Task<IActionResult> Create(CommunityCreateRequest request)
        {
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        public async Task<IActionResult> Update(CommunityUpdateRequest request)
        {
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        public async Task<IActionResult> Delete(CommunityDeleteRequest request)
        {
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        public async Task<IActionResult> Find(CommunityFindRequest request)
        {
            var result = await _mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

    }
}