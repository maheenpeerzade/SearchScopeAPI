using System.ComponentModel.DataAnnotations;

namespace SearchScopeAPI.SearchScope.Core.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }
    }
}
