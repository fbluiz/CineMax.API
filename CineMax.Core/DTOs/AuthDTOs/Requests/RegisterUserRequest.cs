using System.ComponentModel.DataAnnotations;

namespace CineMax.Core.DTOs.AuthDTOs.Requests
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "Field {0} is invalid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [StringLength(50,ErrorMessage = "Field {0} must have between {2} and {1} characters", MinimumLength = 6)]
        public string Password { get; set; }
        [Compare(nameof(Password), ErrorMessage = "As senhas devem ser iguais")]
        public string PasswordConfirmation { get; set; }
    }
}
