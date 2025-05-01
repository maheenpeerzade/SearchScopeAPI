using System.Runtime.Serialization;

namespace SearchScopeAPI.SearchScope.Core.Utility
{
    /// <summary>
    /// SearchResultEnum.
    /// </summary>
    public enum SearchResultEnum
    {
        /// <summary>
        /// Relevance score.
        /// </summary>
        [EnumMember(Value = "Relevance Score")]
        RelevanceScore,

        /// <summary>
        /// Search date.
        /// </summary>
        [EnumMember(Value = "Search Date")]
        SearchDate,

        /// <summary>
        /// Popularity.
        /// </summary>
        [EnumMember(Value = "Popularity")]
        Popularity
    }
}
