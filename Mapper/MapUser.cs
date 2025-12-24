using QuizApp.Dtos;
using QuizApp.Models;

namespace QuizApp.Mapper;

public static class UserMapper
{
    public static GetAllUserResponse MapUser(User user)
    {
        return new GetAllUserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
            RoleId = user.RoleId,
            Role = new RoleResponse
            {
                Id = user.Role.Id,
                Name = user.Role.Name
            }
        };
    }
}
