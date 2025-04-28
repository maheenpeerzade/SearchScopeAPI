using MediatR;
using SearchScopeAPI.SearchScope.Core.Interface;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Queries;
using SearchScopeAPI.SearchScope.Core.Utility;

namespace SearchScopeAPI.SearchScope.Application.Handlers
{
    public class GetSearchHistoryResultHandler : IRequestHandler<GetSearchHistoryResultQuery, IEnumerable<SearchResult>>
    {
        private readonly ISearchResultRepository _searchResultRepository;

        public GetSearchHistoryResultHandler(ISearchResultRepository searchResultRepository)
        {
            _searchResultRepository = searchResultRepository;
        }

        public async Task<IEnumerable<SearchResult>> Handle(GetSearchHistoryResultQuery request, CancellationToken cancellationToken)
        {
            var searchResults = await _searchResultRepository.GetSearchResultsAsync(request.SearchHistoryId);

            if (request.SortBy.HasValue)
            {
                switch (request.SortBy.Value)
                {
                    case SearchResultEnum.RelevanceScore:
                        searchResults = request.IsAscending ? searchResults.OrderBy(sr => sr.RelevanceScore) : searchResults.OrderByDescending(sr => sr.RelevanceScore);
                        break;

                    case SearchResultEnum.SearchDate:
                        searchResults = request.IsAscending ? searchResults.OrderBy(sr => sr.SearchDate) : searchResults.OrderByDescending(sr => sr.SearchDate);
                        break;

                    case SearchResultEnum.Popularity:
                        searchResults = request.IsAscending ? searchResults.OrderBy(sr => sr.Popularity) : searchResults.OrderByDescending(sr => sr.Popularity);
                        break;

                    default: // Fallback sorting by Id
                        searchResults = request.IsAscending ? searchResults.OrderBy(sr => sr.Id) : searchResults.OrderByDescending(sr => sr.Id);
                        break;
                }
            }
            return searchResults;
        }
    }
}
