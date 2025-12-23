using QuizApp.Models;

namespace QuizApp.Interfaces
{
    public interface IOptionsRepository
    {
        Task<int> CreateOptionsAsync(Option option);
    }
}