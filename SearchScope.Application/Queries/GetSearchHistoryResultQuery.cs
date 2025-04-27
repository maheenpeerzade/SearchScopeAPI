using MediatR;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Utility;

namespace SearchScopeAPI.SearchScope.Application.Queries
{
    public class GetSearchHistoryResultQuery : IRequest<IEnumerable<SearchResult>>
    {
        public int SearchHistoryId { get; }
        public SearchResultEnum? SortBy { get; }
        public bool IsAscending { get; }

        public GetSearchHistoryResultQuery(int searchHistoryId, SearchResultEnum? sortBy, bool isAscending)
        {
            SearchHistoryId = searchHistoryId;
            SortBy = sortBy;
            IsAscending = isAscending;
        }
    }
}
