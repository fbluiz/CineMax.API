using System.ComponentModel.DataAnnotations;

namespace CineMax.Core.DTOs.AuthDTOs.Requests
{
    public class UserLoginRequest
    {
        [Required(ErrorMessage = "Field {0} is mandatory")]
        [EmailAddress(ErrorMessage = "O Campo {0} é inválido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field {0} is mandatory")]
        public string Senha { get; set; }
    }
}
