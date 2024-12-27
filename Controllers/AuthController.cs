using ApiGrupoOptico.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGrupoOptico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Validar el modelo recibido
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Datos de entrada inválidos.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }


            try
            {
                // Llamar al servicio de autenticación
                var result = await _authService.AuthenticateAsync(loginDto.Nombre, loginDto.Contrasenia);

                // Si la autenticación falla, retornar 401 Unauthorized
                if (!result.IsAuthenticated)
                {
                    return Unauthorized(new
                    {
                        Message = result.ErrorMessage
                    });
                }

                // Si todo va bien, retornar 200 OK con el token
                return Ok(new
                {
                    Token = result.Token,
                    UserId = result.UserId
                });
            }
            catch (Exception ex)
            {
                // Manejar errores inesperados
                return StatusCode(500, new
                {
                    Message = "Ocurrió un error interno en el servidor.",
                    Details = ex.Message // No recomendado en producción (puede exponer detalles del sistema)
                });
            }

        }

        public class LoginDto
        {
            public string Nombre { get; set; }
            public string Contrasenia { get; set; }
        }
    }
}
