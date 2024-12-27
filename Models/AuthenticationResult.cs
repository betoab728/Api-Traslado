namespace ApiGrupoOptico.Models
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string ErrorMessage { get; set; }
    }
}
