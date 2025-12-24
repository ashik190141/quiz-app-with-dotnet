using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using QuizApp.Models;

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

public class ExamResponse : CreateExamResponse
{
    [JsonPropertyOrder(100)]
    public required GetAllUserResponse CreatedByUser { get; set; }
    [JsonPropertyOrder(101)]
    public required GetAllUserResponse UpdatedByUser { get; set; }
}

public class OptionResponse
{
    public int Id { get; set; }
    public required string Option1 { get; set; }
    public required string Option2 { get; set; }
    public required string Option3 { get; set; }
    public required string Option4 { get; set; }
    public string? CorrectAnswer { get; set; }
    public GetAllUserResponse? CreatedByUser { get; set; }
    public GetAllUserResponse? UpdatedByUser { get; set; }
}

public class GetQuestionResponse
{
    public int Id { get; set; }
    public required string Question { get; set; }
    public int? OptionId { get; set; }
    public  OptionResponse? Option { get; set; }
    public required RandomPatternOption RandomPatternOption { get; set; }
    public GetAllUserResponse? CreatedByUser { get; set; }
    public GetAllUserResponse? UpdatedByUser { get; set; }
}

public class GetQuestionWithOutExamResponse
{
    public ExamResponse? Exam { get; set; }
    public required List<GetQuestionResponse> Question { get; set; }
}

public class RandomPatternOption
{
    public required string Option1 { get; set; }
    public required string Option2 { get; set; }
    public required string Option3 { get; set; }
    public required string Option4 { get; set; }
    public string? CorrectAnswer { get; set; }
}