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
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Archivo> Archivos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuGroup> MenuGroups { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<TipoArticulo> TipoArticulos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoEstado> PedidoEstados { get; set; }
        public DbSet<TipoPago> TipoPagos { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var genericEntities = modelBuilder.Model.GetEntityTypes().ToList();
            foreach (var entityType in genericEntities
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

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Usuarios)
                .UsingEntity("usuario_roles");



            modelBuilder.Entity<Persona>()
                .HasIndex(p => new { p.TipoDocumento, p.Documento })
                .IsUnique();

            modelBuilder.Entity<Permiso>()
                .HasIndex(p => p.Codigo)
                .IsUnique();
            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Permisos)
                .WithMany(p => p.Roles)
                .UsingEntity("rol_permisos");

            modelBuilder.Entity<MenuGroup>()
                .HasOne<Rol>(mg => mg.Rol)
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

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Articulos)
                .WithMany(a => a.Pedidos)
                .UsingEntity("pedido_articulos");
            modelBuilder.Entity<Pedido>()
                .Property(p => p.Fecha)
                .HasColumnName("fecha")
                .HasConversion(
                    // De DateTime a DateOnly (para guardar en BD)
                    v => DateOnly.FromDateTime(v),
                    // De DateOnly a DateTime (para leer de BD)
                    v => v.ToDateTime(TimeOnly.MinValue)
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
