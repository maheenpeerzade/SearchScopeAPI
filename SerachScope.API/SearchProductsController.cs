using MediatR;
using Microsoft.AspNetCore.Mvc;
using SearchScopeAPI.SearchScope.Application.Queries;
using SearchScopeAPI.SearchScope.Core.Utility;
using System.IdentityModel.Tokens.Jwt;

namespace SearchScopeAPI.SerachScope.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CustomLogger _customLogger;

        public SearchProductsController(IMediator mediator, CustomLogger customLogger)
        {
            _mediator = mediator;
            _customLogger = customLogger;
        }

        /// <summary>
        /// This endpoint is used to fetch all products from the database.
        /// </summary>
        /// <param name="query">Specify query.</param>
        /// <param name="filter">Specify filter.</param>
        /// <param name="sortBy">Specify sort by.</param>
        /// <param name="isAscending">Specify sorting order.</param>
        /// <returns>List of products.</returns>
        /// <response code="200">List of products.</response>
        /// <response code="204">Products not available.</response>
        /// <response code="400">Query cannot be empty.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> SearchProducts([FromQuery] string? query, [FromQuery] string? filter, [FromQuery] ProductEnum? sortBy, bool isAscending = true)
        {
            _customLogger.LogInformation("SearchProducts started");
            if (string.IsNullOrWhiteSpace(query))
            {
                _customLogger.LogInformation("SearchProducts completed");
                return BadRequest("Query cannot be empty");
            }

            var result = await _mediator.Send(new SearchProductsQuery(query, filter, sortBy, isAscending));

            // Check for results.
            if (result == null || !result.Any())
            {
                _customLogger.LogInformation("SearchProducts completed");
                return NoContent(); // Returns 204 No Content, if no products are found.
            }

            _customLogger.LogInformation("SearchProducts completed");
            return Ok(result); // Returns 200 OK with the product list
        }

        /// <summary>
        /// This endpoint is used to fetch all search history of logged in user.
        /// </summary>
        /// <param name="isAscending">Specify sorting order.</param>
        /// <returns>List of search history.</returns>
        /// <response code="200">List of products.</response>
        /// <response code="204">Search hsitory not available.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize]
        [HttpGet("SearchHistory")]
        public async Task<IActionResult> GetSearchHistory([FromQuery] bool isAscending = true)
        {
            _customLogger.LogInformation("SearchHistory started");
            string token = Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
            int userId = FetchUserId(token);
            if (userId <= 0)
            {
                _customLogger.LogInformation("SearchHistory completed");
                return Unauthorized("User in unauthorized");
            }

            var histories = await _mediator.Send(new GetSearchHistoryQuery(userId, isAscending));
            // Check for results.
            if (histories == null || !histories.Any())
            {
                _customLogger.LogInformation("SearchHistory completed");
                return NoContent(); // Returns 204 No Content, if no search histories are found.
            }

            _customLogger.LogInformation("SearchHistory completed");
            return Ok(histories);
        }

        /// <summary>
        /// This endpoint is used to fetch all search result by search history id.
        /// </summary>
        /// <param name="searchHistoryId">Specify search history id.</param>
        /// <param name="sortBy">Specify sort by.</param>
        /// <param name="isAscending">Specify sorting order.</param>
        /// <returns>List of search result.</returns>
        /// <response code="200">List of search result.</response>
        /// <response code="204">Search result not available.</response>
        /// <response code="400">Invalid search history id.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize]
        [HttpGet("SearchHistoryResult")]
        public async Task<IActionResult> GetSearchHistoryResult([FromQuery] int searchHistoryId, [FromQuery] SearchResultEnum? sortBy, [FromQuery] bool isAscending = true)
        {
            _customLogger.LogInformation("SearchHistoryResult started");
            if (searchHistoryId <= 0)
            {
                _customLogger.LogInformation("SearchHistoryResult completed");
                return BadRequest("Invalid search history id");
            }

            var searchResults = await _mediator.Send(new GetSearchHistoryResultQuery(searchHistoryId, sortBy, isAscending));

            // Check for results.
            if (searchResults == null || !searchResults.Any())
            {
                _customLogger.LogInformation("SearchHistoryResult completed");
                return NoContent(); // Returns 204 No Content, if no search histories are found.
            }

            _customLogger.LogInformation("SearchHistoryResult completed");
            return Ok(searchResults);
        }

        private static bool IsValidProductEnum<TEnum>(string value) where TEnum : struct, Enum
        {
            // Check if the provided value can be parsed and is defined in the enum
            return Enum.TryParse(value, true, out TEnum result)
                   && Enum.IsDefined(typeof(TEnum), result);
        }

        private static int FetchUserId(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            var userIdd = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimsConstant.UserId)?.Value;
            if (!int.TryParse(userIdd, out var parsedUserId) || parsedUserId == 0)
            {
                return 0;
            }
            return parsedUserId;
            //if (!string.IsNullOrWhiteSpace(token))
            //{
            //    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            //    var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);
            //    var userIdd = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimsConstant.UserId)?.Value;
            //    if (!int.TryParse(userIdd, out var parsedUserId) || parsedUserId == 0)
            //    {
            //        return 0;
            //    }
            //    return parsedUserId;
            //}
            //return 0;
        }
    }
}
