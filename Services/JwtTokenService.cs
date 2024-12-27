using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiGrupoOptico.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
         
        }


        public string GenerateToken(IEnumerable<Claim> claims)
        {
           
            if (claims == null || !claims.Any())
            {
                throw new ArgumentException("Los claims no pueden ser nulos o vacíos.");
            }


            var _jwtSecret = _configuration["Jwt:SecretKey"];
            var _jwtIssuer = _configuration["Jwt:Issuer"];
            var _jwtAudience = _configuration["Jwt:Audience"];


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
