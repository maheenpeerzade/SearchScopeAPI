using MediatR;
using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Core.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Username { get; }
        public string Password { get; }

        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
