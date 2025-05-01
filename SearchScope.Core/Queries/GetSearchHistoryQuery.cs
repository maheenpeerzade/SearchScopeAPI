using MediatR;
using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Queries
{
    /// <summary>
    /// GetSearchHistoryQuery class.
    /// </summary>
    public class GetSearchHistoryQuery : IRequest<IEnumerable<SearchHistory>>
    {
        /// <summary>
        /// User id.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Is ascending.
        /// </summary>
        public bool IsAscending { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userId">Specify user id.</param>
        /// <param name="isAscending">Specify is ascending.</param>
        public GetSearchHistoryQuery(int userId, bool isAscending = true)
        {
            UserId = userId;
            IsAscending = isAscending;
        }
    }
}
