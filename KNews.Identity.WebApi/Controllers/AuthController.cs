using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNews.Identity.Services.Accounts.Handlers;
using KNews.Identity.Services.Authentications.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KNews.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign_in")]
        public async Task<IActionResult> SignIn(EmailPasswordSignInRequest dto)
        {
            var response = await _mediator.Send(dto);
            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(EmailPasswordSignInRequest dto)
        {
            var response = await _mediator.Send(dto);
            return Ok(response);
        }
    }
}