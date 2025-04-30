using System.Runtime.Serialization;

namespace SearchScopeAPI.SearchScope.Core.Utility
{
    public enum SearchResultEnum
    {
        [EnumMember(Value = "Relevance Score")]
        RelevanceScore,

        [EnumMember(Value = "Search Date")]
        SearchDate,

        [EnumMember(Value = "Popularity")]
        Popularity
    }
}
