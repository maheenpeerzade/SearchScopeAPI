using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Interface
{
    public interface ITokenService
    {
        string CreateToken(User user);
        (string Token, DateTime IssuedAt) CreateTokenWithIssuedTime(User user);
    }
}
