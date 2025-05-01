using System.ComponentModel.DataAnnotations;

namespace SearchScopeAPI.SearchScope.Core.Models
{
    /// <summary>
    /// SearchHistory model.
    /// </summary>
    public class SearchHistory
    {
        /// <summary>
        /// HistoryId(Primary Key).
        /// </summary>
        [Key]
        public int HistoryId { get; set; }

        /// <summary>
        /// The term or phrase used in the search.
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string? Keyword { get; set; }

        /// <summary>
        /// Any filters applied during the search.
        /// </summary>
        public string? FilterCriteria { get; set; }

        /// <summary>
        /// The ID of the user who performed the search.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Timestamp of when the search happened.
        /// </summary>
        public DateTime SearchDate { get; set; }

        /// <summary>
        /// Number of results retrieved from the search.
        /// </summary>
        [Range(0, int.MaxValue)]
        public int TotalResults { get; set; }
    }
}
