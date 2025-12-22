using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Question { get; set; }
        [Required]
        public required int ExamId {get; set;}
        [ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; } = null!;
        public int OptionId { get; set; }
        [ForeignKey(nameof(OptionId))]
        public Option Option { get; set; } = null!;
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;
        [ForeignKey(nameof(UpdatedBy))]
        public User UpdatedByUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }
}