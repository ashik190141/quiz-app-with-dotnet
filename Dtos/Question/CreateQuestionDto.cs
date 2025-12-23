using System.ComponentModel.DataAnnotations;

namespace QuizApp.Dtos;

public class CreateQuestionDto
{
    [Required]
    [StringLength(200, MinimumLength = 20, ErrorMessage = "Question must be 20-200 characters")]
    public required string Question { get; set; }
    [Required]
    public required int ExamId { get; set; }
    [Required]
    public required string Option1 { get; set; }
    [Required]
    public required string Option2 { get; set; }
    [Required]
    public required string Option3 { get; set; }
    [Required]
    public required string Option4 { get; set; }
    [Required]
    public required string CorrectAnswer { get; set; }
}