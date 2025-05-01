using System.ComponentModel.DataAnnotations;
namespace SearchScopeAPI.SearchScope.Core.Models
{
    /// <summary>
    /// SearchResult model.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Id(Primary Key).
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Links to the Product entry.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Links to SearchHistory entry.
        /// </summary>
        public int SearchHistoryId { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Unit Price of the Product
        /// </summary>
        public decimal? UnitPrice { get; set; }

        /// <summary>
        /// Indicates if the product is discontinued.
        /// </summary>
        public bool Discontinued { get; set; }

        /// <summary>
        /// Search term used.
        /// </summary>
        public string? Keyword { get; set; }

        /// <summary>
        /// Popularity metric (fractional).
        /// </summary>
        [Range(1, 10)]
        public decimal Popularity { get; set; }

        /// <summary>
        /// Timestamp of the search.
        /// </summary>
        public DateTime SearchDate { get; set; }

        /// <summary>
        /// Match's relevance score.
        /// </summary>
        public double RelevanceScore { get; set; }
    }
}
