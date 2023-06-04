using System.Text.Json.Serialization;

namespace CineMax.Core.DTOs.AuthDTOs.Responses
{
    public class UserLoginResponse
    {
        public bool Sucess { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; private set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DataExpiracao { get; private set; }
        public List<string> Erros { get; private set; }

        public UserLoginResponse() =>
            Erros = new List<string>();

        public UserLoginResponse(bool sucess = true) : this() =>
            Sucess = sucess;
        public UserLoginResponse(bool sucess, string token, DateTime dataExpiracao) : this(sucess) 
        {
            Token = token;
            DataExpiracao = dataExpiracao;

        }

        public void AddErro(string erro) =>
            Erros.Add(erro); 

        public void AddErros(IEnumerable<string> erros) =>
            Erros.AddRange(erros);
            
    }
}
