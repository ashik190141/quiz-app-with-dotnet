using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Models;

namespace QuizApp.Interfaces
{
    public interface IQuestionRepository
    {
        Task<Questions> CreateQuestionAsync(Questions questions);
        Task<int> GetQuestionCountByExamIdAsync(int id);
        Task<IEnumerable<Questions>> GetQuestionsByExamIdAsync(int id, int roleId);
    }

    public interface IQuestionService
    {
        Task<StandardApiResponse<CreateQuestionResponse?>> CreateQuestionAsync(CreateQuestionDto createQuestionDto, bool isTeacher, string userId);
        Task<StandardApiResponse<GetQuestionWithOutExamResponse>> GetExamQuestionAsync(int examId, string userId);
    }
}