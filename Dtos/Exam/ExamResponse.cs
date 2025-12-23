using System.Text.Json.Serialization;
using QuizApp.Dtos;

namespace QuizApp.Models
{
    public class CreateExamResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Year { get; set; }
        public required Month Month { get; set; }
        public required string Duration { get; set; }
        public required int NumberOfQuestions { get; set; }
        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }

    public class GetAllExamResponse : CreateExamResponse
    {
        [JsonPropertyOrder(100)]
        public required CreateUserResponse CreatedByUser { get; set; }
        [JsonPropertyOrder(101)]
        public required CreateUserResponse UpdatedByUser { get; set; }
    }
}