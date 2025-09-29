using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }

        [Display(Name = "Skill Level")]
        public string? SkillLevel { get; set; }

        public double Tuition { get; set; }

        // Navigation properties
    }
}
