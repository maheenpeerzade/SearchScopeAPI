using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchScopeAPI.SearchScope.Core.Commands;
using SearchScopeAPI.SerachScope.API.Logger;

namespace SearchScopeAPI.SerachScope.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CustomLogger _customLogger;

        public AccountController(IMediator mediator, CustomLogger customLogger)
        {
            _mediator = mediator;
            _customLogger = customLogger;
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
            _customLogger.LogInformation("Login started.");
            if (command == null || string.IsNullOrEmpty(command?.Username) || string.IsNullOrEmpty(command?.Password))
            {
                _customLogger.LogWarning("Username or password cannot be empty.");
                return BadRequest("Username or password cannot be empty.");
            }

            var response = await _mediator.Send(command);
            if (response.Token == null)
            {
                _customLogger.LogWarning(response.Message);
                return Unauthorized(new { response.Message });
            }

            _customLogger.LogInformation("Login completed.");
            return Ok(response);
        }
    }
}
