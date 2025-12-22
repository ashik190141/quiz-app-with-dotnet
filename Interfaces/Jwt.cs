using QuizApp.Models;

namespace QuizApp.Interfaces;
public interface ITokenService
{
    string GenerateToken(User user);
}