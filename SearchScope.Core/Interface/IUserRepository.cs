using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interface
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string username, string password);
    }
}
