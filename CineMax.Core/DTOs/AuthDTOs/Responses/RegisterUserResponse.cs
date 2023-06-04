using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CineMax.Core.DTOs.AuthDTOs.Responses
{
    public class RegisterUserResponse
    {
        public bool Success { get; private set; }
        public List<String> Erros { get; set; }

        public RegisterUserResponse() => 
            Erros = new List<String>(); 

        public RegisterUserResponse(bool sucess = true) : this() =>
            Success = sucess;

        public void AddErros (IEnumerable<string> erros) =>
            Erros.AddRange(erros);
    }
}
