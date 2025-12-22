namespace QuizApp.Dtos;

public class LoginResponse
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Token { get; set; }
    public required int RoleId { get; set; }
}