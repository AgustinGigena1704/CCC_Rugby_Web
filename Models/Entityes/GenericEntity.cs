using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCC_Rugby_Web.Models.Entityes
{
    public class GenericEntity
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        [DefaultValue(null)]
        public DateTime UpdatedAt { get; set; }
        [Column("deleted_at")]
        [DefaultValue(null)]
        public DateTime? DeletedAt { get; set; } = null;
        [Required]
        [Column("created_by")]
        public int CreatedBy { get; set; }
        public required Usuario CreatedByUsuario { get; set; }
        [Column("updated_by")]
        [DefaultValue(null)]
        public int UpdatedBy { get; set; }
        public Usuario? UpdatedByUsuario { get; set; }
        [Column("deleted_by")]
        [DefaultValue(null)]
        public int? DeletedBy { get; set; } = null;
        public Usuario? DeletedByUsuario { get; set; } = null;
        [Required]
        [DefaultValue(false)]
        [Column("borrado_logico")]
        public bool BorradoLogico { get; set; } = false;
    }
}
