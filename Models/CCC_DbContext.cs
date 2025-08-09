using CCC_Rugby_Web.Models.Entityes;
using Microsoft.EntityFrameworkCore;

namespace CCC_Rugby_Web.Models
{
    public class CCC_DbContext : DbContext
    {
        public CCC_DbContext(DbContextOptions<CCC_DbContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRol> UsuarioRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Archivo> Archivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                         .Where(t => typeof(GenericEntity).IsAssignableFrom(t.ClrType)))
            {
                var entity = modelBuilder.Entity(entityType.ClrType);
                entity.HasOne(typeof(Usuario), nameof(GenericEntity.CreatedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(GenericEntity.CreatedBy))
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(typeof(Usuario), nameof(GenericEntity.UpdatedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(GenericEntity.UpdatedBy))
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(typeof(Usuario), nameof(GenericEntity.DeletedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(GenericEntity.DeletedBy))
                    .OnDelete(DeleteBehavior.SetNull);
            }

            modelBuilder.Entity<Usuario>()
                .HasOne<Persona>(u => u.Persona)
                .WithMany()
                .HasForeignKey(u => u.PersonaId);
            modelBuilder.Entity<Usuario>()
                .HasOne<Archivo>(u => u.AvatarArchivo)
                .WithMany()
                .HasForeignKey(u => u.AvatarArchivoId);
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<Usuario>()
                .HasOne<Usuario>(u => u.DeletedByUsuario)
                .WithMany()
                .HasForeignKey(u => u.DeletedBy);
            modelBuilder.Entity<Usuario>()
                .HasOne<Usuario>(u => u.UpdatedByUsuario)
                .WithMany()
                .HasForeignKey(u => u.UpdatedBy);
            

            modelBuilder.Entity<Persona>()
                .HasIndex(p => new { p.TipoDocumento, p.Documento })
                .IsUnique();

            modelBuilder.Entity<UsuarioRol>()
                .HasOne<Usuario>(ur => ur.Usuario)
                .WithMany()
                .HasForeignKey(ur => ur.UsuarioId);
            modelBuilder.Entity<UsuarioRol>()
                .HasOne<Role>(ur => ur.Role)
                .WithMany()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Permiso>()
                .HasIndex(p => p.Codigo)
                .IsUnique();

            modelBuilder.Entity<RolPermiso>()
                .HasOne<Role>(rp => rp.Rol)
                .WithMany()
                .HasForeignKey(rp => rp.RolId);
            modelBuilder.Entity<RolPermiso>()
                .HasOne<Permiso>(rp => rp.Permiso)
                .WithMany()
                .HasForeignKey(rp => rp.PermisoId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
