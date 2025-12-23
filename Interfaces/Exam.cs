using QuizApp.Dtos;
using QuizApp.Helper;
using QuizApp.Models;

namespace QuizApp.Interfaces
{
    public interface IExamRepository
    {
        Task<Exam> CreateExamAsync(Exam exam);
        Task<IEnumerable<Exam>> GetAllExamAsync();
        Task<bool> GetExamExistAsync(string examName, string year);
        Task<Exam?> GetExamByIdAsync(int id);
    }

    public interface IExamService
    {
        Task<StandardApiResponse<CreateExamResponse?>> CreateExamAsync(CreateExamDto createExamDto, bool isTeacher, string userId);
        Task<StandardApiResponse<IEnumerable<GetAllExamResponse>>> GetAllExamAsync();
    }
}