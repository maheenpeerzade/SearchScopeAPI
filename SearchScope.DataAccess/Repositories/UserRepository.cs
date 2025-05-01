using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interfaces;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.DataAccess.Data;

namespace SearchScopeAPI.SearchScope.DataAccess.Repositories
{
    /// <summary>
    /// UserRepository class.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly SearchScopeDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">Specify SearchScopeDbContext.</param>
        public UserRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// To get user by credentials.
        /// </summary>
        /// <param name="username">Specify username.</param>
        /// <param name="password">Specify password.</param>
        /// <returns>User.</returns>
        public async Task<User?> GetUserAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
