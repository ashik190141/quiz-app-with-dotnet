using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuizApp.Data;
using QuizApp.Models;

public class DatabaseSeeder
{
    private readonly AdminUserSettings _adminSettings;
    private readonly QuizContext _context;
    public DatabaseSeeder(QuizContext context ,IOptions<AdminUserSettings> adminSettings)
    {
        _adminSettings = adminSettings.Value;
        _context = context;
    }
    public async Task SeedAsync()
    {
        var adminUser = new User
        {
            Name = _adminSettings.Name,
            Email = _adminSettings.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(_adminSettings.Password),
            RoleId = _adminSettings.RoleId
        };

        if (!await _context.Roles.AnyAsync())
        {
            var roles = Enum.GetValues(typeof(Roles))
                .Cast<Roles>()
                .Select(r => new Role
                {
                    Id = (int)r,
                    Name = r.ToString()
                });

            await _context.Roles.AddRangeAsync(roles);
            await _context.SaveChangesAsync();
        }

        var findSuperAdmin = await _context.Users.Where(u => u.RoleId == (int)Roles.SuperAdmin).CountAsync();
        if(findSuperAdmin == 0)
        {
            await _context.Users.AddAsync(adminUser);
            await _context.SaveChangesAsync();
        }
    }
}
