using QuizApp.Models;

namespace QuizApp.Dtos;

public class CreateUserResponse
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int RoleId { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime UpdatedAt { get; set; }
}

public class GetAllUserResponse : CreateUserResponse
{
    public required Role Role { get; set; }
}