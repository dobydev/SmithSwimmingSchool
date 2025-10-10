using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.Models
{
    public class Coach
    {
        public int CoachId { get; set; }

        [Display(Name = "Coach Name")]
        public required string CoachName { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        // Navigation properties
        public ICollection<Session> Sessions { get; set; } = new List<Session>();

        public string? ApplicationUserId { get; set; }

        public string? Bio { get; set; }

        public string? Certifications { get; set; }
    }
}
