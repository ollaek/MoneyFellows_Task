using MF_Task.Service.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MF_Task.Service.Commands
{
    public enum UserRole
    {
        User,
        Admin
    }
    public class RegisterUserCommand : BaseCommand<BaseResponseDTO<object>>
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Username must be alphanumeric.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Phone number must be a valid Egyptian mobile number.")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }
        [JsonIgnore] 
        public UserRole Role { get; set; }
    }
}
