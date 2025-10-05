using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.Models
{
    public class Swimmer
    {
        public int SwimmerId { get; set; }

        [Required]
        [Display(Name = "Swimmer Name")]
        public string? SwimmerName { get; set; }

        public string? Gender { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Birth Date")]
        public string? DateOfBirth { get; set; }

        // Navigation properties
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
