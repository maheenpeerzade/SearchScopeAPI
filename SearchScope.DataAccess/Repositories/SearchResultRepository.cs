using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.DataAccess.Data;

namespace SearchScopeAPI.SearchScope.DataAccess.Repositories
{
    /// <summary>
    /// SearchResultRepository class.
    /// </summary>
    public class SearchResultRepository : ISearchResultRepository
    {
        private readonly SearchScopeDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Specify SearchScopeDbContext.</param>
        public SearchResultRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// To get search results by seach history id.
        /// </summary>
        /// <param name="searchHistoryId">Specify search history id.</param>
        /// <returns>SearchResults.</returns>
        public async Task<IEnumerable<SearchResult>> GetSearchResultsAsync(int searchHistoryId)
        {
            return await _context.SearchResults.Where(sr => sr.SearchHistoryId == searchHistoryId).ToListAsync();
        }
    }
}
