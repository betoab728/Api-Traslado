
using ApiGrupoOptico.Models;

namespace ApiGrupoOptico.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResult> AuthenticateAsync(string nombre, string contrasenia);
    }
}
