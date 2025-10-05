using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmithSwimmingSchool.ViewModels
{
    // ViewModel for user registration
    public class AccountRegisterViewModel
    {
        // Properties with validation attributes
        [Required(ErrorMessage = "First name is required.")]
        [MaxLength(128, ErrorMessage = "First name max length is 128 characters.")]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [MaxLength(128, ErrorMessage = "Last name max length is 128 characters.")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [NotMapped]
        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {LastName}";

        [Required(ErrorMessage = "Email is required.")]
        [MaxLength(256, ErrorMessage = "Email max length is 256 characters.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6–20 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Please confirm the password.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be 6–20 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public required string ConfirmPassword { get; set; }
    }
}
