using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.ViewModels
{
    public class CoachProfileViewModel
    {
        [Required, Display(Name = "Coach Name")]
        public string? CoachName { get; set; }

        [Phone, Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Bio { get; set; }

        public string? Certifications { get; set; }
    }
}