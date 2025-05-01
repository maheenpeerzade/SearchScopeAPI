using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interfaces
{
    /// <summary>
    /// IUserRepository interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// To get user by credentials.
        /// </summary>
        /// <param name="username">Specify username.</param>
        /// <param name="password">Specify password.</param>
        /// <returns>User.</returns>
        Task<User?> GetUserAsync(string username, string password);
    }
}
