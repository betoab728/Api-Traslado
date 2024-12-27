
using ApiGrupoOptico.Models;
using Microsoft.Data.SqlClient;
using System.Data;


namespace ApiGrupoOptico.Services
{
    public class TiendaService : ITiendaService
    {
        private readonly string _connectionString;

        public TiendaService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<Tienda>> ListarTiendasAsync()
        {
            var tiendas = new List<Tienda>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("ListarTiendas", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tiendas.Add(new Tienda
                {
                    idtienda = reader.GetInt32(reader.GetOrdinal("idtienda")),
                    nombreTienda = reader.GetString(reader.GetOrdinal("nombre_tienda"))
                });
            }

            return tiendas;

        }

        public async Task<List<Tienda>> ListarAlmacenesDestinoAsync(int idAlmacenOrigen)
        {
            var almacenesDestino = new List<Tienda>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("ListarAlmacenesDestino", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@idalmacen", idAlmacenOrigen);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                almacenesDestino.Add(new Tienda
                {
                    idtienda = reader.GetInt32(reader.GetOrdinal("idtienda")),
                    nombreTienda = reader.GetString(reader.GetOrdinal("nombre_tienda"))
                });
            }

            return almacenesDestino;
        }
        public async Task<List<Vitrina>> ListarVitrinasAsync(int idAlmacenDestino)
        {
            var vitrinas = new List<Vitrina>();
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("ListarVitrinaTienda", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@idalmacen", idAlmacenDestino);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                vitrinas.Add(new Vitrina
                {
                    IdVitrina = reader.GetInt32(reader.GetOrdinal("idvitrina")),
                    NombreVitrina = reader.GetString(reader.GetOrdinal("nombre"))
                });
            }

            return vitrinas;
        }
    }


}
