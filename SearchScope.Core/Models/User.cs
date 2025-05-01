using System.ComponentModel.DataAnnotations;

namespace SearchScopeAPI.SearchScope.Core.Models
{
    /// <summary>
    /// User model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id(Primary Key).
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string? Email { get; set; }
    }
}
