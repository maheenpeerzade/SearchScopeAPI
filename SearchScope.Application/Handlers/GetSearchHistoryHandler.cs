using MediatR;
using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Core.Queries;

namespace SearchScopeAPI.SearchScope.Application.Handlers
{
    public class GetSearchHistoryHandler : IRequestHandler<GetSearchHistoryQuery, IEnumerable<SearchHistory>>
    {
        private readonly ISearchHistoryRepository _searchHistoryRepository;

        public GetSearchHistoryHandler(ISearchHistoryRepository searchHistoryRepository)
        {
            _searchHistoryRepository = searchHistoryRepository;
        }

        public async Task<IEnumerable<SearchHistory>> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _searchHistoryRepository.GetSearchHistoriesAsync(request.UserId, request.IsAscending);
        }
    }
}
