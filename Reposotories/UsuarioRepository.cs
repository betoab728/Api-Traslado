using ApiGrupoOptico.Data;
using ApiGrupoOptico.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace ApiGrupoOptico.Reposotories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();

        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);

        }

        public async Task UpdateAsync(Usuario usuario)
        {
           
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
          
        }

        public async Task<Usuario?> FindByNombreAsync(string nombre)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.nombre == nombre);
        }
    }
}
