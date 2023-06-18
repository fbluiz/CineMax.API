using CineMax.API.Controllers;
using CineMax.Application.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace CineMax.API.Attributes
{
    public class ExtractUserIdFilter
    {
        public static string ExtractUserIdFromToken(HttpRequest request)
        {
            string token = request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            string userId = jwtToken.Claims.First(claim => claim.Type == "sub").Value;

            return userId;
        }
    }
}
