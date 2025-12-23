using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Exam name must be 5-50 characters")]
        public required string Name { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Exam year must be in 4 digits")]
        public required string Year { get; set; }
        [Required]
        public required Month Month { get; set; }
        public required string Duration { get; set; }
        public required int NumberOfQuestions { get; set; }
        [Required]
        public required DateTime StartTime { get; set; }
        [Required]
        public required DateTime EndTime { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;
        [ForeignKey(nameof(UpdatedBy))]
        public User UpdatedByUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }

    public enum Month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
}