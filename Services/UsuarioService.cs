using ApiGrupoOptico.Models;
using ApiGrupoOptico.Reposotories;

namespace ApiGrupoOptico.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync() => await _repository.GetAllAsync();

        public async Task<Usuario?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Usuario usuario)
        {
            //encriptacion de la contrasenia
            usuario.contrasenia =BCrypt.Net.BCrypt.HashPassword(usuario.contrasenia);
            await _repository.AddAsync(usuario);
        } 

        public async Task UpdateAsync(Usuario usuario) => await _repository.UpdateAsync(usuario);

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}
