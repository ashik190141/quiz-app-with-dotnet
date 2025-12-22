using System.ComponentModel.DataAnnotations;
using QuizApp.Models;

namespace QuizApp.Dtos;

public class ExamDto
{
    [Required]
    [Range(5,50,ErrorMessage ="Exam name must be in 5-50 words")]
    public required string Name { get; set; }
    [Required]
    [MinLength(4, ErrorMessage = "Exam year must be in 4 digits")]
    public required string Year { get; set; }
    [Required]
    public required Month Month { get; set; }
    public required int NumberOfQuestions { get; set; }
    [Required]
    public required DateTime StartTime { get; set; }
    [Required]
    public required DateTime EndTime { get; set; }
}