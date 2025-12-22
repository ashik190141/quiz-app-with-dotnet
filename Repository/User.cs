using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Interfaces;
using QuizApp.Models;

namespace QuizApp.Repositories
{
    public class UserRepository(QuizContext context) : BaseRepository(context), IUserRepository
    {
        public async Task<User> AddUserAsync(User user)
        {
            var addUser = await _context.Users.AddAsync(user);
            await SaveChangesAsync();
            return addUser.Entity;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }
    }
}