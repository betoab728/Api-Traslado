
using ApiGrupoOptico.Models;
using ApiGrupoOptico.Reposotories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ApiGrupoOptico.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtTokenService _jwtTokenService;


        public AuthService(IUsuarioRepository usuarioRepository, IJwtTokenService jwtTokenService  )
        {
            _usuarioRepository = usuarioRepository;
            _jwtTokenService = jwtTokenService;

        }

        public async Task<AuthenticationResult> AuthenticateAsync(string nombre, string contrasenia)
        {
            try
            {
                var usuario = await _usuarioRepository.FindByNombreAsync(nombre);

                // Verificar si el usuario existe y si la contraseña es correcta
              
                Console.WriteLine(usuario.idusuario);
                Console.WriteLine(usuario.nombre);

                if (usuario == null)
                {
                    return new AuthenticationResult
                    {
                        IsAuthenticated = false,
                        ErrorMessage = "Usuario no encontrado."
                    };
                }

                if (!VerifyPassword(contrasenia, usuario.contrasenia))
                {
                    return new AuthenticationResult
                    {
                        IsAuthenticated = false,
                        ErrorMessage = "Contraseña incorrecta."
                    };
                }

                // Generar un token JWT si la autenticación es correcta
                
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.idusuario.ToString()),
                    new Claim(ClaimTypes.Name, usuario.nombre),

                   
                };

                var token = _jwtTokenService.GenerateToken(claims);

                return new AuthenticationResult
                {
                    IsAuthenticated = true,
                    UserId = usuario.idusuario.ToString(),
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    IsAuthenticated = false,
                    ErrorMessage = ex.Message
                };
            }
        }


        private bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }

    }
}
