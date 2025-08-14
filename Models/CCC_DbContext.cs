using CCC_Rugby_Web.Models.Entityes;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

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
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<RolPermiso> RolPermisos { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                         .Where(t => typeof(IGenericEntityWithCreated).IsAssignableFrom(t.ClrType)))
            {
                var entity = modelBuilder.Entity(entityType.ClrType);

                entity.HasOne(typeof(Usuario), nameof(IGenericEntityWithCreated.CreatedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(IGenericEntityWithCreated.CreatedBy))
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(typeof(Usuario), nameof(IGenericEntityWithCreated.UpdatedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(IGenericEntityWithCreated.UpdatedBy))
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(nameof(IGenericEntityWithCreated.UpdatedAt))
                    .HasDefaultValue(null);
                entity.Property(nameof(IGenericEntityWithCreated.UpdatedBy))
                    .HasDefaultValue(null);

                entity.HasOne(typeof(Usuario), nameof(IGenericEntityWithCreated.DeletedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(IGenericEntityWithCreated.DeletedBy))
                    .OnDelete(DeleteBehavior.SetNull);
                entity.Property(nameof(IGenericEntityWithCreated.DeletedAt))
                    .HasDefaultValue(null);
                entity.Property(nameof(IGenericEntityWithCreated.DeletedBy))
                    .HasDefaultValue(null);

                entity.Property(nameof(IGenericEntityWithCreated.BorradoLogico))
                    .HasDefaultValue(false);
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                         .Where(t => typeof(IGenericEntity).IsAssignableFrom(t.ClrType) &&
                                    !typeof(IGenericEntityWithCreated).IsAssignableFrom(t.ClrType)))
            {
                var entity = modelBuilder.Entity(entityType.ClrType);

                entity.HasOne(typeof(Usuario), nameof(IGenericEntity.UpdatedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(IGenericEntity.UpdatedBy))
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(nameof(IGenericEntity.UpdatedAt))
                    .HasDefaultValue(null);
                entity.Property(nameof(IGenericEntity.UpdatedBy))
                    .HasDefaultValue(null);

                entity.HasOne(typeof(Usuario), nameof(IGenericEntity.DeletedByUsuario))
                    .WithMany()
                    .HasForeignKey(nameof(IGenericEntity.DeletedBy))
                    .OnDelete(DeleteBehavior.SetNull);
                entity.Property(nameof(IGenericEntity.DeletedAt))
                    .HasDefaultValue(null);
                entity.Property(nameof(IGenericEntity.DeletedBy))
                    .HasDefaultValue(null);

                entity.Property(nameof(IGenericEntity.BorradoLogico))
                    .HasDefaultValue(false);
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
                .HasForeignKey(ur => ur.RoleId);

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

            modelBuilder.Entity<MenuGroup>()
                .HasOne<Role>(mg => mg.Rol)
                .WithMany()
                .HasForeignKey(mg => mg.RolId);
            modelBuilder.Entity<MenuGroup>()
                .Property(mg => mg.RolId)
                .HasDefaultValue(null);
            modelBuilder.Entity<MenuGroup>()
                .Property(mg => mg._icono)
                .HasDefaultValue(Icons.Material.Filled.List);

            modelBuilder.Entity<MenuItem>()
                .HasOne<MenuGroup>(mi => mi.MenuGrupo)
                .WithMany()
                .HasForeignKey(mi => mi.MenuGrupoId);
            modelBuilder.Entity<MenuItem>()
                .Property(mi => mi._icono)
                .HasDefaultValue(Icons.Material.Filled.List);
            modelBuilder.Entity<MenuItem>()
                .HasIndex(mi => mi.Codigo)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
