using CineMax.Core.Auth.Configurations;
using CineMax.Core.DTOs.AuthDTOs.Requests;
using CineMax.Core.DTOs.AuthDTOs.Responses;
using CineMax.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CineMax.Core.Auth
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly UserManager<IdentityUser> _userManager;

        private readonly JwtOptions _jwtOptions;

        public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<JwtOptions> jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<UserLoginResponse> Login(UserLoginRequest userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Senha, false, true);
            if (result.Succeeded)
                return await GenerateToken(userLogin.UserName);

            var usuarioLoginResponse = new UserLoginResponse(result.Succeeded);
            if(!result.Succeeded)
            {
                if (result.IsLockedOut)
                    usuarioLoginResponse.AddErro("This account is blocked");
                else if (result.IsNotAllowed)
                    usuarioLoginResponse.AddErro("This account does not have permission to do this.");
                else if (result.RequiresTwoFactor)
                    usuarioLoginResponse.AddErro("it is necessary to confirm the login in your email");
                else
                    usuarioLoginResponse.AddErro("Incorrect username or password");
            }

            return usuarioLoginResponse;
        }

        public async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest registerUser)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerUser.Name,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(identityUser, registerUser.Password);
            if (result.Succeeded)
            {
                await _userManager.SetLockoutEnabledAsync(identityUser, false);
                // Verifique se a propriedade "Role" está definida na classe RegisterUserRequest
                if (!string.IsNullOrEmpty(registerUser.Role))
                    await _userManager.AddToRoleAsync(identityUser, registerUser.Role);
            }

            var usuarioCadastroResponse = new RegisterUserResponse(success:result.Succeeded,role: registerUser.Role,userId: Guid.Parse(identityUser.Id));
            if (!result.Succeeded && result.Errors.Count() > 0)
                usuarioCadastroResponse.AddErros(result.Errors.Select(r => r.Description));
           
            return usuarioCadastroResponse; 
        }

        public async Task<UserLoginResponse> GenerateToken (string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var tokenClaims = await GetClaims(user);

            var dataExpiracao = DateTime.Now.AddSeconds(_jwtOptions.Expiration);

            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: tokenClaims,
                notBefore: DateTime.Now,
                expires: dataExpiracao,
                signingCredentials: _jwtOptions.SigningCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new UserLoginResponse(sucess: true, token: token, dataExpiracao: dataExpiracao); 
        }

        public async  Task<IList<Claim>> GetClaims (IdentityUser User)
        {
            var claims = await _userManager.GetClaimsAsync(User);
            var roles = await _userManager.GetRolesAsync(User);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, User.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, User.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            return claims;
        }
    }
}