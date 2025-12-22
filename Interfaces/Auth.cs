using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Models;

namespace QuizApp.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> LoginAsync(LoginDto loginDto);
    }

    public interface IAuthService
    {
        Task<StandardApiResponse<LoginResponse?>> LoginAsync(LoginDto loginDto);
    }
}