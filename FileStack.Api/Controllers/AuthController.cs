using FileStack.Application.User.Command.UserEmailConfirmation;
using FileStack.Application.User.Command.UserLoginCommand;
using FileStack.Application.User.Command.UserREgisterationCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileStack.Api.Controllers
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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCommand command)
        {
            var response= await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPost("verify-email")]
        public async Task <IActionResult> VerifyAccount([FromBody] ConfirmEmailCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);

        }

    }
}
