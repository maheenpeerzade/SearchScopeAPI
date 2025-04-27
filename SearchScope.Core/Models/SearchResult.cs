using System.ComponentModel.DataAnnotations;
namespace SearchScopeAPI.SearchScope.Core.Models
{
    public class SearchResult
    {
        public int Id { get; set; } // Unique ID for the search result
        public int ProductId { get; set; } // Links to the Product entry
        public int SearchHistoryId { get; set; } // Links to SearchHistory entry
        public string? Name { get; set; } // Product Name
        public decimal? UnitPrice { get; set; } // Unit Price of the Product
        public bool Discontinued { get; set; } // Indicates if the product is discontinued
        public string? Keyword { get; set; } // Search term used
        [Range(1, 10)]
        public decimal Popularity { get; set; } // Popularity metric (fractional)
        public DateTime SearchDate { get; set; } // Timestamp of the search
        public double RelevanceScore { get; set; } // Match's relevance score
    }
}
