using QuizApp.Data;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Repositories;

public class OptionRepository(QuizContext context) : BaseRepository(context), IOptionsRepository
{
    public async Task<int> CreateOptionsAsync(Option option)
    {
        await _context.Options.AddAsync(option);
        int res = await _context.SaveChangesAsync();
        if(res > 0) return option.Id;
        else return 0;
    }
}