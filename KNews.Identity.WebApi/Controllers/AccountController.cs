using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KNews.Identity.Services.Accounts.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KNews.Identity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, ILogger<AccountController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AccountRegistrationRequest dto)
        {
            await _mediator.Send(dto);
            return Ok();
        }

        [HttpPost("confirm_email")]
        public async Task<IActionResult> ConfirmByEmail(AccountEmailConfirmationRequest dto)
        {
            await _mediator.Send(dto);
            return Ok();
        }

        [HttpPost("confirm_phone")]
        public async Task<IActionResult> ConfirmByPhone(AccountPhoneConfirmationRequest dto)
        {
            await _mediator.Send(dto);
            return Ok();
        }

        [HttpPost("punish")]
        public async Task<IActionResult> Punish(AccountPunishRequest dto)
        {
            await _mediator.Send(dto);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(AccountDeleteRequest dto)
        {
            await _mediator.Send(dto);
            return Ok();
        }

        [HttpDelete("change_password")]
        public async Task<IActionResult> ChangePassword(AccountChangePasswordRequest dto)
        {
            await _mediator.Send(dto);
            return Ok();
        }
    }
}