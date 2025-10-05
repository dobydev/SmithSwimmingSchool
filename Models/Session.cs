using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.Models
{
    public class Session
    {
        public int SessionId { get; set; }

        // FKs
        public int LessonId { get; set; }
        public int CoachId { get; set; }

        [Display(Name = "Seat Capacity")]
        public int SeatCapacity { get; set; }

        [Display(Name = "Start Time")]
        public DateTimeOffset DailyStartTime { get; set; }

        [Display(Name = "Start Date")]
        public DateTimeOffset StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTimeOffset EndDate { get; set; }

        // Navigation properties
        public Lesson? Lesson { get; set; }
        public Coach? Coach { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
