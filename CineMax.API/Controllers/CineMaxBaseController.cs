using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IdentityModel.Tokens.Jwt;

namespace CineMax.API.Controllers
{
    public class CineMaxBaseController : ControllerBase
    {
        [NonAction]
        public virtual Guid ExtractUserIdFromToken()
        {

            HttpRequest request = HttpContext.Request ;

            string token = request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            string userId = jwtToken.Claims.First(claim => claim.Type == "sub").Value;

            return Guid.Parse(userId);
        }
    }
}