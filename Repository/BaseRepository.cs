using QuizApp.Data;

namespace QuizApp.Repositories;

public class BaseRepository
{
    protected readonly QuizContext _context;

    protected BaseRepository(QuizContext context)
    {
        _context = context;
    }

    protected async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}