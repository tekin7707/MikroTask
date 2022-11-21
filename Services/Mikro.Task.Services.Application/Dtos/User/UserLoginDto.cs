using System.ComponentModel.DataAnnotations;

namespace Mikro.Task.Services.Application.Dtos.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Please enter an email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DataType(DataType.Password)]
        [StringLength(128, ErrorMessage = "Password must have {2} character", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
