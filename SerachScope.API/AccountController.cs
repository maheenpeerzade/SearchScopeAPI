using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchScopeAPI.SearchScope.Application.Commands;
using SearchScopeAPI.SearchScope.Core.Interface;

namespace SearchScopeAPI.SerachScope.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITokenService _tokenService;

        public AccountController(IMediator mediator, ITokenService tokenService)
        {
            _mediator = mediator;
            _tokenService = tokenService;
        }

        /// <summary>
        /// This endpoint is used to authenticate the user and return a JWT token.
        /// </summary>
        /// <param name="command">Specify username and password.</param>
        /// <returns>JWT token if authentication is successful, or an error message.</returns>
        /// <response code="200">JWT token.</response>
        /// <response code="400">Username and password cannot be empty.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            if (command == null || string.IsNullOrEmpty(command?.Username) || string.IsNullOrEmpty(command?.Password))
                return BadRequest("Username or password cannot be empty.");

            var response = await _mediator.Send(command);
            if (response.Token == null)
            {
                return Unauthorized(new { Message = response.Message });
            }

            return Ok(response);
        }
    }
}
