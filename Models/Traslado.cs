namespace ApiGrupoOptico.Models
{
    public class Traslado
    {
        public int IdProducto { get; set; }
        public int IdAlmacenOrigen { get; set; }
        public int IdAlmacenDestino { get; set; }
        public int Cantidad { get; set; }
        public string Vitrina { get; set; } = string.Empty;
        public int IdUsuario { get; set; }
    }
}
