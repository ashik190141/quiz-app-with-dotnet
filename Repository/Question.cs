using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Repositories;

public class QuestionRepository(QuizContext context) : BaseRepository(context), IQuestionRepository
{
    public async Task<Questions> CreateQuestionAsync(Questions question)
    {
        var addQuestion = await _context.Questions.AddAsync(question);
        await SaveChangesAsync();
        return addQuestion.Entity;
    }

    public async Task<int> GetQuestionCountByExamIdAsync(int id)
    {
        var questions = await _context.Questions.Where(x => x.ExamId == id).ToListAsync();
        return questions.Count;
    }

    public async Task<IEnumerable<Questions>> GetQuestionsByExamIdAsync(int id, int roleId)
    {
        var query = _context.Questions
        .Where(q => q.ExamId == id)
        .Include(q => q.Exam)
            .ThenInclude(e => e.CreatedByUser)
                .ThenInclude(u => u.Role)
        .Include(q => q.Option)
        .AsQueryable();

        if (roleId == (int)Roles.Student)
        {
            query = query.OrderBy(q => EF.Functions.Random());
        }

        return await query.ToListAsync();
    }
}