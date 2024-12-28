
using ApiGrupoOptico.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ApiGrupoOptico.Services
{
    public class TrasladoService : ITrasladoService
    {
    
        private readonly string _connectionString;

        public TrasladoService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task RealizarTrasladoAsync(Traslado traslado)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("TrasladoMovil", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@idproducto", traslado.IdProducto);
            command.Parameters.AddWithValue("@idalmacenorigen", traslado.IdAlmacenOrigen);
            command.Parameters.AddWithValue("@idalmacendestino", traslado.IdAlmacenDestino);
            command.Parameters.AddWithValue("@cantidad", traslado.Cantidad);
            command.Parameters.AddWithValue("@vitrina", traslado.Vitrina);
            command.Parameters.AddWithValue("@idusuario", traslado.IdUsuario);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

        }

        public async Task<Producto> BuscarProductoAsync(string codigoBarras, int idAlmacen)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("BuscarProductoCodigoBarras", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@codigo", codigoBarras);
            command.Parameters.AddWithValue("@idalmacen", idAlmacen);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Producto
                {
                    Idproducto = reader.GetInt32(reader.GetOrdinal("idproducto")),
                    Descripcion = reader.GetString(reader.GetOrdinal("producto")),
                    Stock = reader.GetInt32(reader.GetOrdinal("stock")),
                    Codigo = reader.GetString(reader.GetOrdinal("codigo"))

                };
            }
            return null;
        }
    }
    
}
