using System.Security.Claims;

namespace ApiGrupoOptico.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
