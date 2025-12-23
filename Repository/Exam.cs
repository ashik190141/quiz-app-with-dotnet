using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Dtos;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Repositories
{
    public class ExamRepository(QuizContext context) : BaseRepository(context), IExamRepository
    {
        public async Task<Exam> CreateExamAsync(Exam exam)
        {
            var addExam = await _context.Exams.AddAsync(exam);
            await SaveChangesAsync();
            return addExam.Entity;
        }

        public async Task<IEnumerable<Exam>> GetAllExamAsync()
        {
            var exams = from s in _context.Exams orderby s.CreatedAt descending select new Exam
            {
                Id = s.Id,
                Name = s.Name,
                Year = s.Year,
                Month = s.Month,
                Duration = s.Duration,
                NumberOfQuestions = s.NumberOfQuestions,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                CreatedAt = s.CreatedAt,
                CreatedBy = s.CreatedBy,
                CreatedByUser = s.CreatedByUser,
                UpdatedAt = s.UpdatedAt,
                UpdatedBy = s.UpdatedBy,
                UpdatedByUser = s.UpdatedByUser
            };
            
            return await exams.ToListAsync();
        }

        public async Task<bool> GetExamExistAsync(string examName, string year)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Name == examName && e.Year == year);
            if(exam == null) return false;
            return true;
        }
    }
}