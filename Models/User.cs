using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;

public class User
{
    [Key]
    public int Id {get; set;}
    [Required]
    public required string Name {get; set;}
    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email {get; set;}
    [Required]
    public required string Password {get; set;}
    [Required]
    public required int RoleId {get; set;}
    [ForeignKey((nameof(RoleId)))]
    public Role Role {get; set;} = null!;
    public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
}

public class AdminUserSettings
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int RoleId { get; set; }
}