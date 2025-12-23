using System.ComponentModel.DataAnnotations;

namespace QuizApp.Dtos;

public class CreateQuestionResponse
{
    public int Id { get; set; }
    public required string Question { get; set; }
    public required int ExamId { get; set; }
    public string Option1 { get; set; } = string.Empty;
    public string Option2 { get; set; } = string.Empty;
    public string Option3 { get; set; } = string.Empty;
    public string Option4 { get; set; } = string.Empty;
    public string CorrectAnswer { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}