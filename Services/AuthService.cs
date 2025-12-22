using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Interfaces;

namespace QuizApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }

        public async Task<StandardApiResponse<LoginResponse?>> LoginAsync(LoginDto loginDto)
        {
            var user = await _authRepository.LoginAsync(loginDto);
            if(user == null)
            {
                return new StandardApiResponse<LoginResponse?>(false, "User do not found", null);
            }
            if(!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return new StandardApiResponse<LoginResponse?>(false, "Password do not match", null);
            }

            var token = _tokenService.GenerateToken(user);

            var response = new LoginResponse
            {
                Name = user.Name,
                Email = user.Email,
                Token = token,
                RoleId = user.RoleId
            };

            return new StandardApiResponse<LoginResponse?>(true, "Login successful", response);
        }
    }
}