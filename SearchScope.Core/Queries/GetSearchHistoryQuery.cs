using MediatR;
using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Queries
{
    public class GetSearchHistoryQuery : IRequest<IEnumerable<SearchHistory>>
    {
        public int UserId { get; }
        public bool IsAscending { get; }

        public GetSearchHistoryQuery(int userId, bool isAscending = true)
        {
            UserId = userId;
            IsAscending = isAscending;
        }
    }
}
