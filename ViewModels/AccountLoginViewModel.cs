using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.ViewModels
{
    public class AccountLoginViewModel

    {
        [Required, EmailAddress]
        public required string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }

    }
}