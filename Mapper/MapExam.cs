using QuizApp.Dtos;
using QuizApp.Models;

namespace QuizApp.Mapper;

public static class ExamMapper
{
    public static ExamResponse MapExam(Exam exam)
    {
        return new ExamResponse
        {
            Id = exam.Id,
            Name = exam.Name,
            Year = exam.Year,
            Month = exam.Month,
            Duration = exam.Duration,
            NumberOfQuestions = exam.NumberOfQuestions,
            StartTime = exam.StartTime,
            EndTime = exam.EndTime,
            CreatedBy = exam.CreatedBy,
            UpdatedBy = exam.UpdatedBy,
            CreatedAt = exam.CreatedAt,
            UpdatedAt = exam.UpdatedAt,
            CreatedByUser = UserMapper.MapUser(exam.CreatedByUser),
            UpdatedByUser = UserMapper.MapUser(exam.UpdatedByUser)
        };
    }

}
