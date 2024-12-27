using ApiGrupoOptico.Models;

namespace ApiGrupoOptico.Services
{
    public interface ITiendaService
    {
        Task<List<Tienda>> ListarTiendasAsync();
        Task<List<Tienda>> ListarAlmacenesDestinoAsync(int idAlmacenOrigen);
        Task<List<Vitrina>> ListarVitrinasAsync(int idAlmacenDestino);
    }
}
