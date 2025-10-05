using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        [Required]
        [Display(Name = "Skill Level")]
        public string? SkillLevel { get; set; }

        [DataType(DataType.Currency)]
        public double Tuition { get; set; }

        // Foreign keys
        [Display(Name = "Coach")]
        public int? CoachId { get; set; }

        [Display(Name = "Swimmer")]
        public int? SwimmerId { get; set; }

        // Navigation properties
        public virtual Coach? Coach { get; set; }
        public virtual Swimmer? Swimmer { get; set; }

        // Navigation properties for related entities
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
