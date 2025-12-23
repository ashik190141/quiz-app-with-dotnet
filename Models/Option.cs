using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        public required string Option1 { get; set; }
        public required string Option2 { get; set; }
        public required string Option3 { get; set; }
        public required string Option4 { get; set; }
        public required string CorrectAnswer { get; set; }
        public int CreatedBy { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public User CreatedByUser { get; set; } = null!;
        public int UpdatedBy { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        public User UpdatedByUser { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        public DateTime UpdatedAt { get; set; } = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
    }
}