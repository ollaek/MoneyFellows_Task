using MediatR;
using MF_Task.Service.Commands;
using MF_Task.Service.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MF_Task.Presentation_API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            registerCommand.Role = UserRole.User;
            var response = await _mediator.Send(registerCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
        [HttpPost("registerAdmin")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserCommand registerCommand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            registerCommand.Role = UserRole.Admin;
            var response = await _mediator.Send(registerCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _mediator.Send(loginQuery);
            if (response.Success)
            {
                return Ok(new { Token = response.Data });
            }

            return Unauthorized(new { Message = response.Message });
        }
    }
}