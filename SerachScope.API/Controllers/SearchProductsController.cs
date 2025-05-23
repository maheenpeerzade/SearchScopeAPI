﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchScopeAPI.SearchScope.Core.Queries;
using SearchScopeAPI.SearchScope.Core.Utility;
using SearchScopeAPI.SerachScope.API.Logger;
using System.IdentityModel.Tokens.Jwt;

namespace SearchScopeAPI.SerachScope.API.Controllers
{
    /// <summary>
    /// SearchProductsController class.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SearchProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CustomLogger _customLogger;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="mediator">Specify mediator.</param>
        /// <param name="customLogger">Specify CustomLogger.</param>
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
        /// <param name="pageNumber">Specify page number.</param>
        /// <param name="pageSize">Specify page size.</param>
        /// <returns>List of products.</returns>
        /// <response code="200">List of products.</response>
        /// <response code="204">Products not available.</response>
        /// <response code="400">Query cannot be empty.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> SearchProducts([FromQuery] string? query, [FromQuery] string? filter, [FromQuery] ProductEnum? sortBy, bool isAscending = true, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                _customLogger.LogInformation("SearchProducts started");
                if (string.IsNullOrWhiteSpace(query))
                {
                    _customLogger.LogWarning("Query cannot be empty");
                    return BadRequest("Query cannot be empty");
                }

                //Check for valid page number and size
                if (pageNumber <= 0 || pageSize <= 0)
                {
                    _customLogger.LogWarning("Invalid page number and page size");
                    return BadRequest("Please enter valid PageNumber and PageSize. Both must be greater than 0.");
                }

                var result = await _mediator.Send(new SearchProductsQuery(query, filter, sortBy, isAscending, pageNumber, pageSize));

                // Check for results.
                if (result == null || !result.Any())
                {
                    _customLogger.LogWarning("No products are found.");
                    return NoContent(); // Returns 204 No Content, if no products are found.
                }

                _customLogger.LogInformation("SearchProducts completed");
                return Ok(result); // Returns 200 OK with the product list
            }
            catch (Exception ex)
            {
                _customLogger.LogError(ex, "Failed to fetch products.");
                return StatusCode(500, "Failed to fetch products.");
            }
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
        [HttpGet("SearchHistory")]
        public async Task<IActionResult> GetSearchHistory([FromQuery] bool isAscending = true)
        {
            try
            {
                _customLogger.LogInformation("SearchHistory started");
                string token = Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
                int userId = FetchUserId(token);
                if (userId <= 0)
                {
                    _customLogger.LogWarning("User is unauthorized");
                    return Unauthorized("User is unauthorized");
                }

                var histories = await _mediator.Send(new GetSearchHistoryQuery(userId, isAscending));
                // Check for results.
                if (histories == null || !histories.Any())
                {
                    _customLogger.LogWarning("No search histories are found");
                    return NoContent(); // Returns 204 No Content, if no search histories are found.
                }

                _customLogger.LogInformation("SearchHistory completed");
                return Ok(histories);
            }
            catch (Exception ex)
            {
                _customLogger.LogError(ex, "Failed to fetch search history.");
                return StatusCode(500, "Failed to fetch search history.");
            }
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
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("SearchHistoryResult")]
        public async Task<IActionResult> GetSearchHistoryResult([FromQuery] int searchHistoryId, [FromQuery] SearchResultEnum? sortBy, [FromQuery] bool isAscending = true)
        {
            try
            {
                _customLogger.LogInformation("SearchHistoryResult started");
                string token = Request.Headers.Authorization.ToString().Replace("Bearer ", string.Empty);
                int userId = FetchUserId(token);
                if (userId <= 0)
                {
                    _customLogger.LogWarning("User is unauthorized");
                    return Unauthorized("User is unauthorized");
                }

                if (searchHistoryId <= 0)
                {
                    _customLogger.LogWarning($"Invalid search history id {searchHistoryId}.");
                    return BadRequest($"Invalid search history id {searchHistoryId}.");
                }

                var searchResults = await _mediator.Send(new GetSearchHistoryResultQuery(searchHistoryId, sortBy, isAscending));

                // Check for results.
                if (searchResults == null || !searchResults.Any())
                {
                    _customLogger.LogWarning("No search histories are found.");
                    return NoContent(); // Returns 204 No Content, if no search histories are found.
                }

                _customLogger.LogInformation("SearchHistoryResult completed");
                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                _customLogger.LogError(ex, "Failed to fetch search history result.");
                return StatusCode(500, "Failed to fetch search history result.");
            }
        }

        private static int FetchUserId(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(token);
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                if (!int.TryParse(userId, out var parsedUserId) || parsedUserId == 0)
                {
                    return 0;
                }
                return parsedUserId;
            }
            return 0;
        }
    }
}
