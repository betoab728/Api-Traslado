
using Microsoft.EntityFrameworkCore;
using ApiGrupoOptico.Models;


namespace ApiGrupoOptico.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                // Configurar clave primaria
                entity.HasKey(u => u.idusuario);

                // Configurar el nombre de la tabla (si es diferente al nombre de la clase)
                entity.ToTable("usuarios_v2");
            });
        }
    }
}
