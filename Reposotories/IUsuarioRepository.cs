using ApiGrupoOptico.Models;

namespace ApiGrupoOptico.Reposotories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);

        Task <Usuario> FindByNombreAsync(string nombre);
    }
}
