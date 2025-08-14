using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    // Interface que reemplaza a IBaseProperties
    public interface IGenericEntity
    {
        int Id { get; set; }
        DateTime? UpdatedAt { get; set; }
        int? UpdatedBy { get; set; }
        Usuario? UpdatedByUsuario { get; set; }
        bool BorradoLogico { get; set; }
        DateTime? DeletedAt { get; set; }
        int? DeletedBy { get; set; }
        Usuario? DeletedByUsuario { get; set; }
    }

    // Interface extendida para entidades con created
    public interface IGenericEntityWithCreated
    {
        int Id { get; set; }
        DateTime CreatedAt { get; set; }
        int CreatedBy { get; set; }
        Usuario CreatedByUsuario { get; set; }
        DateTime? UpdatedAt { get; set; }
        int? UpdatedBy { get; set; }
        Usuario? UpdatedByUsuario { get; set; }
        bool BorradoLogico { get; set; }
        DateTime? DeletedAt { get; set; }
        int? DeletedBy { get; set; }
        Usuario? DeletedByUsuario { get; set; }
    }

    // Marcadores para indicar si incluir propiedades de creación
    public interface IWithCreated { }
    public interface IWithoutCreated { }

    public struct WithCreated : IWithCreated { }
    public struct WithoutCreated : IWithoutCreated { }

    // Clase base para todas las entidades
    public abstract class GenericEntityBase : IGenericEntity
    {
        [Key]
        [Required]
        [Column("id", Order = 0)]
        public int Id { get; set; }

        [Required]
        [Column("updated_at", Order = 995)]
        [DefaultValue(null)]
        public DateTime? UpdatedAt { get; set; }

        [Required]
        [Column("updated_by", Order = 996)]
        [DefaultValue(null)]
        public int? UpdatedBy { get; set; }
        public Usuario? UpdatedByUsuario { get; set; }

        [Column("deleted_at", Order = 997)]
        [DefaultValue(null)]
        public DateTime? DeletedAt { get; set; } = null;

        [Column("deleted_by", Order = 998)]
        [DefaultValue(null)]
        public int? DeletedBy { get; set; } = null;
        public Usuario? DeletedByUsuario { get; set; } = null;

        [Required]
        [DefaultValue(false)]
        [Column("borrado_logico", Order = 999)]
        public bool BorradoLogico { get; set; } = false;
    }

    // GenericEntity SIN propiedades de creación
    public abstract class GenericEntity<T> : GenericEntityBase where T : IWithoutCreated
    {
        // Esta clase hereda todas las propiedades de GenericEntityBase
        // No agrega propiedades de creación
    }

    // GenericEntity CON propiedades de creación
    public abstract class GenericEntityWithCreation<T> : GenericEntityBase, IGenericEntityWithCreated where T : IWithCreated
    {
        [Required]
        [Column("created_at", Order = 993)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("created_by", Order = 994)]
        public int CreatedBy { get; set; }
        public required Usuario CreatedByUsuario { get; set; }
    }

    // Alias para mantener compatibilidad (CON created)
    public abstract class GenericEntity : GenericEntityWithCreation<WithCreated>
    {
    }
}