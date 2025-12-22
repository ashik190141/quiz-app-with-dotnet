using System.ComponentModel.DataAnnotations;

namespace QuizApp.Dtos;

public class CreateUserDto
{
    [Required]
    [StringLength(30, MinimumLength = 3, 
        ErrorMessage = "User name must be between 3 and 30 characters")]
    public required string Name { get; set; }
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}

public class UpdateUserDto
{
    [StringLength(30, MinimumLength = 3, 
        ErrorMessage = "User name must be between 3 and 30 characters")]
    public string? Name { get; set; }
}