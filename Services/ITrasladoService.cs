

using ApiGrupoOptico.Models;

namespace ApiGrupoOptico.Services

{
    public interface ITrasladoService
    {
        Task RealizarTrasladoAsync(Traslado traslado);

        //para buscar un producto por su codigo de barras y id de almacen
        Task<Producto> BuscarProductoAsync(string codigoBarras, int idAlmacen);
    }
}
