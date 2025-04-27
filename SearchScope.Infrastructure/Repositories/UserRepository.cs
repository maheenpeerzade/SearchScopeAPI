using Microsoft.EntityFrameworkCore;
using SearchScopeAPI.SearchScope.Core.Interface;
using SearchScopeAPI.SearchScope.Core.Models;
using SearchScopeAPI.SearchScope.Infrastructure.Data;

namespace SearchScopeAPI.SearchScope.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SearchScopeDbContext _context;

        public UserRepository(SearchScopeDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
