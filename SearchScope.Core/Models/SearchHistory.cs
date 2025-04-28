using System.ComponentModel.DataAnnotations;

namespace SearchScopeAPI.SearchScope.Core.Models
{
    public class SearchHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string? Keyword { get; set; } // The term or phrase used in the search

        public string? FilterCriteria { get; set; } // Any filters applied during the search ??

        [Required]
        public int UserId { get; set; } // The ID of the user who performed the search

        public DateTime SearchDate { get; set; } // Timestamp of when the search happened

        [Range(0, int.MaxValue)]
        public int TotalResults { get; set; } // Number of results retrieved from the search
    }
}
