using MediatR;
using SearchScopeAPI.SearchScope.Application.Commands;
using SearchScopeAPI.SearchScope.Core.Interface;
using SearchScopeAPI.SearchScope.Core.Models;

namespace SearchScopeAPI.SearchScope.Application.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserAsync(request.Username, request.Password);
            if (user == null)
            {
                return new LoginResponse
                {
                    Message = "Username or password is incorrect.",
                    Token = null
                };
            }

            var token = _tokenService.CreateToken(user);
            return new LoginResponse
            {
                Message = "Login successful!",
                Token = token
            };
        }
    }
}
