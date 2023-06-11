using Newtonsoft.Json;

namespace CineMax.Core.DTOs.AuthDTOs.Responses
{
    public class RegisterUserResponse
    {
        public bool Success { get; private set; }
        public List<string> Erros { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }

        public RegisterUserResponse(bool success, string role, Guid userId)
        {
            Success = success;
            Role = role;
            UserId = userId;
            Erros= new List<string>();
        }

        public void AddErros (IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
