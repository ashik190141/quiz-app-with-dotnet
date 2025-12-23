using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Models;

namespace QuizApp.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Questions> CreateQuestionAsync(Questions questions);
        Task<int> GetQuestionCountByExamIdAsync(int id);
        Task<IEnumerable<Questions>> GetQuestionsByExamIdAsync(int id);
    }

    public interface IQuestionService
    {
        Task<StandardApiResponse<CreateQuestionResponse?>> CreateQuestionAsync(CreateQuestionDto createQuestionDto, bool isTeacher, string userId);
        Task<StandardApiResponse<IEnumerable<Questions>>> GetExamQuestionAsync(int examId, string userId);
    }
}